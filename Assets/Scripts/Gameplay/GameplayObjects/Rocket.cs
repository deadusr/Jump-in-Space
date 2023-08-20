using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class Rocket : MonoBehaviour {
        public Action rocketCrushed;

        [SerializeField]
        Transform rocket;

        [SerializeField]
        float baseRocketSpeed = 10f;

        [SerializeField]
        float boostMultiplier = 1.5f;

        [SerializeField]
        float boostTime = 1f;

        [CanBeNull]
        Planet planetWithActingGravity;
        [CanBeNull]
        Planet lastPlanetWithActingGravity;

        bool rocketLaunched;
        int steeringRotation = -1; // -1 right, 1 left
        float boostSpeed;

        float distancePassed;
        float rocketSpeed;
        bool boosted;

        Vector2 startPosition;

        float timePassedOnPlanet;


        void Start() {
            InputController.Instance.Interact += OnInteract;
            rocketSpeed = baseRocketSpeed;
            startPosition = rocket.position;
        }


        void OnDestroy() {
            InputController.Instance.Interact -= OnInteract;
        }

        void OnInteract() {
            if (rocketLaunched) {
                if (planetWithActingGravity)
                    Boost();
                else
                    ApplyBoost(3f);
            }
            else {
                LaunchRocket();
            }
        }

        public void LaunchRocket() {
            if (!rocketLaunched) {
                rocketLaunched = true;
                Boost();
            }
        }

        public void BlowUp() {

            Debug.Log("BOOM!!!");
            // Destroy(this);
        }

        public void Stop() {
            rocketSpeed = 0;
            boostSpeed = 0;
        }

        public void Land(Vector2 landingPos) {
            rocketSpeed = 0;
            boostSpeed = 0;
        }

        public void Crush() {
            rocketCrushed?.Invoke();
        }

        public void ApplyBoost(float duration) {
            StartCoroutine(BoostRocket(duration, true));
        }

        void Boost() {
            StartCoroutine(LeavePlanetGravity());
            StartCoroutine(BoostRocket(boostTime));
        }


        IEnumerator BoostRocket(float duration, bool isBoosted = false) {
            float targetSpeed = rocketSpeed * boostMultiplier;
            float t = 0;
            boosted = isBoosted;
            while (t < duration) {
                boostSpeed = GetBoostSpeed(t, targetSpeed, duration);
                t += Time.deltaTime;
                yield return null;
            }
            boostSpeed = 0;
            boosted = false;
        }


        float GetBoostSpeed(float t, float maxSpeed, float duration) {
            float timeToAccelerate = duration * 0.2f;
            float timeToDecelerate = duration * 0.8f;

            if (t < timeToAccelerate) {
                return (t / timeToAccelerate) * maxSpeed;
            }
            if (t > duration - timeToDecelerate) {
                float t2 = t - (duration - timeToDecelerate);
                return maxSpeed - (t2 / timeToDecelerate) * maxSpeed;
            }
            return maxSpeed;
        }

        IEnumerator LeavePlanetGravity() {
            lastPlanetWithActingGravity = planetWithActingGravity;
            planetWithActingGravity = null;
            transform.parent = null;

            yield return new WaitForSeconds(1f);
            lastPlanetWithActingGravity = null;
        }

        void Update() {
            if (rocketLaunched) {
                float finalSpeed = rocketSpeed + boostSpeed;

                if (!planetWithActingGravity && !boosted) {
                    planetWithActingGravity = FindPlanetWithActingGravity();

                    if (planetWithActingGravity)
                        timePassedOnPlanet = 0f;
                    // if (planetWithActingGravity) {
                    //     var planetPos = planetWithActingGravity.transform.position;
                    //     Vector2 dirToRocketFromPlanet = (rocket.position - planetPos).normalized;
                    //     steeringRotation = MathFG.WedgeProduct(dirToRocketFromPlanet, rocket.up) < 0 ? -1 : 1;
                    // }
                }

                if (planetWithActingGravity) {
                    transform.parent = planetWithActingGravity.transform;

                    var planetPos = planetWithActingGravity.transform.position;
                    Vector2 dirToRocketFromPlanet = (rocket.position - planetPos).normalized;
                    float angleSpeedRad =
                        (finalSpeed / planetWithActingGravity.GravityRadius) *
                        steeringRotation * planetWithActingGravity.GravityAngularSpeed *
                        (planetWithActingGravity.RedPlanet ? (1f + timePassedOnPlanet * 0.25f) : 1);
                    Vector2 rotatedDir = Quaternion.Euler(0, 0, angleSpeedRad * Time.deltaTime * Mathf.Rad2Deg) * dirToRocketFromPlanet;

                    Vector3 nextPosition = (Vector3)(rotatedDir * planetWithActingGravity.GravityRadius) + planetPos;
                    CalculateDistancePassed();

                    rocket.right = rotatedDir * steeringRotation;
                    rocket.position = nextPosition;

                    timePassedOnPlanet += Time.deltaTime;
                }
                else {
                    Vector3 nextPosition = rocket.position + rocket.up * (Time.deltaTime * finalSpeed);
                    CalculateDistancePassed();
                    rocket.position = nextPosition;
                }
            }

        }

        void CalculateDistancePassed() {
            float xPassed = rocket.position.x - startPosition.x;
            if (xPassed > distancePassed) {
                distancePassed = xPassed;
            }
        }


        [CanBeNull]
        Planet FindPlanetWithActingGravity() {
            foreach (var planet in FindObjectsOfType<Planet>()) {
                if (!ReferenceEquals(lastPlanetWithActingGravity, planet) && planet.InsideGravity(rocket.position)) {
                    return planet;
                }
            }
            return null;
        }

    }
}