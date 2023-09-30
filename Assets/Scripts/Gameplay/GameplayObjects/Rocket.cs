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
        
        float baseRocketSpeed;
        float boostMultiplier;
        float boostTime;

        bool lockedControls;
        bool isRocketLaunched;
        
        [CanBeNull]
        Planet planetWithActingGravity;
        [CanBeNull]
        Planet lastPlanetWithActingGravity;

        float fuelLevel = 100f;
        float distancePassed;
        float rocketSpeed;
        float boostSpeed;
        bool boosted;

        Transform rocketTf;
        Vector2 startPosition;
        List<Planet> planets;

        public float FuelLevel => fuelLevel;
        public bool Boosted => boosted;

        public bool LockedControls {
            set => lockedControls = value;
        }

        void Awake() {
            planets = new List<Planet>();
            foreach (var planet in FindObjectsOfType<Planet>()) {
                planets.Add(planet);
            }
        }

        void OnEnable() {
            InputController.Instance.Interact += OnInteract;
        }

        void OnDisable() {
            InputController.Instance.Interact -= OnInteract;
        }


        public void SetupRocket(RocketSpec specs) {
            baseRocketSpeed = specs.RocketSpeed;
            boostMultiplier = specs.BoostMultiplier;
            boostTime = specs.BoostTime;
        }


        void Start() {
            rocketSpeed = baseRocketSpeed;
            startPosition = transform.position;
        }

        void OnInteract() {
            if (lockedControls)
                return;

            if (isRocketLaunched) {
                if (planetWithActingGravity) {
                    LeavePlanet();
                    Boost();
                }
            }
            else {
                LaunchRocket();
                Boost();
                rocketLaunched?.Invoke();
            }
        }

        public void LaunchRocket() {
            isRocketLaunched = true;
        }

        public void BlowUp() {
            Debug.Log("BOOM!!!");
        }

        public void StopRocket() {
            rocketSpeed = 0;
            boostSpeed = 0;
        }

        public void LandRocket(Vector2 landingPos) {
            rocketSpeed = 0;
            boostSpeed = 0;
            isRocketLaunched = false;
        }

        public void Crush() {
            rocketCrushed?.Invoke();
        }
        void Boost() {
            StartCoroutine(BoostRocket(boostTime));
        }

        void LeavePlanet() {
            StartCoroutine(LeavePlanetGravity());
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

            if (isRocketLaunched) {
                float fuelConsumptionPerSecond = 15f;
                fuelLevel = Mathf.Max(0, fuelLevel - Time.deltaTime * fuelConsumptionPerSecond);

                if (fuelLevel == 0) {
                    Crush();
                }

                if (!planetWithActingGravity) {
                    planetWithActingGravity = FindPlanetWithActingGravity();

                    if (planetWithActingGravity) {
                        fuelLevel = 100f;
                        transform.parent = planetWithActingGravity.transform;
                    }
                }

                var nextPosition =
                    planetWithActingGravity ?
                        GetNextPositionWhenInsidePlanetGravity(finalSpeed)
                        : GetNextPositionWhenOutsidePlanetGravity(finalSpeed);

                CalculateDistancePassed();
                transform.position = nextPosition;
            }


        }

        Vector3 GetNextPositionWhenInsidePlanetGravity(float speed) {
            var planetPos = planetWithActingGravity.transform.position;
            Vector2 dirToRocketFromPlanet = (transform.position - planetPos).normalized;
            
            float angleSpeedRad =
                (speed / planetWithActingGravity.GravityRadius) *
                planetWithActingGravity.RotationDirection * planetWithActingGravity.GravityAngularSpeed;
            
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angleSpeedRad * Time.deltaTime * Mathf.Rad2Deg) * dirToRocketFromPlanet;

            Vector3 nextPosition = (Vector3)(rotatedDir * planetWithActingGravity.GravityRadius) + planetPos;
            transform.right = rotatedDir * planetWithActingGravity.RotationDirection;
            return nextPosition;
        }

        Vector3 GetNextPositionWhenOutsidePlanetGravity(float speed) {
            Vector3 nextPosition = transform.position + transform.up * (Time.deltaTime * speed);
            return nextPosition;
        }

        void CalculateDistancePassed() {
            float xPassed = transform.position.x - startPosition.x;
            if (xPassed > distancePassed) {
                distancePassed = xPassed;
            }
        }


        [CanBeNull]
        Planet FindPlanetWithActingGravity() {
            foreach (var planet in planets) {
                if (!ReferenceEquals(lastPlanetWithActingGravity, planet) && planet.InsideGravity(transform.position)) {
                    return planet;
                }
            }
            return null;
        }

    }
}