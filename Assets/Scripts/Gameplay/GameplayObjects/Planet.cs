using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class Planet : MonoBehaviour {
        [SerializeField]
        float planetRadius;

        [SerializeField]
        [Range(0, 10)]
        float gravityRadius;

        [SerializeField]
        [Range(1, 3)]
        float gravityAngularSpeed = 1f;
        
        [SerializeField]
        [Tooltip("Planet that accelerates rocket while rocket inside it's orbit")]
        bool redPlanet;

        Vector2 planetToRocket;
        [CanBeNull]
        PlanetTrajectory trajectory;

        public float PlanetRadius => planetRadius;
        public float GravityRadius => gravityRadius;
        public float GravityAngularSpeed => gravityAngularSpeed;

        public bool RedPlanet => redPlanet;

        void OnDrawGizmos() {
#if UNITY_EDITOR
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.forward, planetRadius, 1f);

            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.forward, gravityRadius, 1f);
#endif
        }


        void Start() {
            if (transform.parent && transform.parent.TryGetComponent(out PlanetTrajectory parentTrajectory)) {
                trajectory = parentTrajectory;
            }
        }

        void Update() {
            if (trajectory) {
                transform.position = trajectory.GetNextPosition(transform.position);
            }
        }

        public bool InsideGravity(Vector2 objectPosition) {
            float distance = Vector2.Distance(transform.position, objectPosition);
            return distance < gravityRadius;
        }
    }

}