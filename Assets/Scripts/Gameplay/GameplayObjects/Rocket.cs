using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {

    public enum RocketMode {
        Controlled,
        Boosted
    }

    public class Rocket : MonoBehaviour {
        public Action rocketCrushed;
        public Action rocketLaunched;

        [SerializeField]
        Transform rocket;

        [SerializeField]
        float baseRocketSpeed = 10f;

        [SerializeField]
        float boostMultiplier = 1.5f;

        [SerializeField]
        float boostTime = 1f;

        [SerializeField]
        RocketMode mode;

        [CanBeNull]
        Planet planetWithActingGravity;
        [CanBeNull]
        Planet lastPlanetWithActingGravity;

        bool isRocketLaunched;
        int steeringRotation = -1; // -1 right, 1 left
        float boostSpeed;

        float distancePassed;
        float rocketSpeed;
        bool boosted;

        float fuelLevel = 100f;

        Vector2 startPosition;

        float timePassedOnPlanet;

        public float FuelLevel => fuelLevel;
        public bool Boosted => boosted;

        List<Planet> planets;

        void Awake() {
            planets = new List<Planet>();
            foreach (var planet in FindObjectsOfType<Planet>()) {
                planets.Add(planet);
            }
        }


        void Start() {
            InputController.Instance.Interact += OnInteract;
            rocketSpeed = baseRocketSpeed;
            startPosition = rocket.position;
        }


        void OnDestroy() {
            InputController.Instance.Interact -= OnInteract;
        }

        void OnInteract() {
            if (isRocketLaunched) {
                if (planetWithActingGravity)
                    Boost();
            }
            else {
                LaunchRocket();
                rocketLaunched?.Invoke();
            }
        }

        public void LaunchRocket() {
            if (!isRocketLaunched) {
                isRocketLaunched = true;
                Boost();
            }
        }
        
        public void ApplyBoost(float duration) {
            StartCoroutine(BoostRocket(duration, true));
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
            isRocketLaunched = false;
        }

        public void Crush() {
            rocketCrushed?.Invoke();
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
            float finalSpeed = rocketSpeed + boostSpeed;

            if (mode == RocketMode.Boosted) {
                rocket.position = GetNextPositionWhenOutsidePlanetGravity(finalSpeed);
            }

            else {
                if (isRocketLaunched) {
                    fuelLevel = Mathf.Max(0, fuelLevel - Time.deltaTime * 15f);

                    if (fuelLevel == 0) {
                        Crush();
                    }

                    if (!planetWithActingGravity) {
                        planetWithActingGravity = FindPlanetWithActingGravity();

                        if (planetWithActingGravity) {
                            timePassedOnPlanet = 0f;
                            fuelLevel = 100f;
                        }
                        // if (planetWithActingGravity) {
                        //     var planetPos = planetWithActingGravity.transform.position;
                        //     Vector2 dirToRocketFromPlanet = (rocket.position - planetPos).normalized;
                        //     steeringRotation = MathFG.WedgeProduct(dirToRocketFromPlanet, rocket.up) < 0 ? -1 : 1;
                        // }
                    }

                    if (planetWithActingGravity) {
                        transform.parent = planetWithActingGravity.transform;
                        timePassedOnPlanet += Time.deltaTime;
                        var nextPosition = GetNextPositionWhenInsidePlanetGravity(finalSpeed);
                        CalculateDistancePassed();
                        rocket.position = nextPosition;
                    }
                    else {
                        var nextPosition = GetNextPositionWhenOutsidePlanetGravity(finalSpeed);
                        CalculateDistancePassed();
                        rocket.position = nextPosition;
                    }
                }
            }

        }

        Vector3 GetNextPositionWhenInsidePlanetGravity(float speed) {
            if (!planetWithActingGravity)
                return rocket.position;

            var planetPos = planetWithActingGravity.transform.position;
            Vector2 dirToRocketFromPlanet = (rocket.position - planetPos).normalized;
            float angleSpeedRad =
                (speed / planetWithActingGravity.GravityRadius) *
                planetWithActingGravity.RotationDirection * planetWithActingGravity.GravityAngularSpeed *
                (planetWithActingGravity.RedPlanet ? (1f + timePassedOnPlanet * 0.25f) : 1);
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angleSpeedRad * Time.deltaTime * Mathf.Rad2Deg) * dirToRocketFromPlanet;

            Vector3 nextPosition = (Vector3)(rotatedDir * planetWithActingGravity.GravityRadius) + planetPos;

            rocket.right = rotatedDir * planetWithActingGravity.RotationDirection;
            return nextPosition;
        }

        Vector3 GetNextPositionWhenOutsidePlanetGravity(float speed) {
            Vector3 nextPosition = rocket.position + rocket.up * (Time.deltaTime * speed);
            return nextPosition;
        }

        void CalculateDistancePassed() {
            float xPassed = rocket.position.x - startPosition.x;
            if (xPassed > distancePassed) {
                distancePassed = xPassed;
            }
        }


        [CanBeNull]
        Planet FindPlanetWithActingGravity() {
            foreach (var planet in planets) {
                if (!ReferenceEquals(lastPlanetWithActingGravity, planet) && planet.InsideGravity(rocket.position)) {
                    return planet;
                }
            }
            return null;
        }

    }
}