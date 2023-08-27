using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class PlanetTrajectory : MonoBehaviour {

        [SerializeField]
        float speed = 7f;

        [SerializeField]
        [Range(0, 10)]
        float trajectoryRadius = 2f;

        [SerializeField]
        [Range(-1, 1)]
        int rotation = -1;


#if UNITY_EDITOR
        void OnDrawGizmos() {
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, Vector3.forward, trajectoryRadius);
        }
#endif

        void Start() {
            gameObject.DrawCircle(trajectoryRadius, 0.2f, Color.gray);
        }

        public Vector3 GetNextPosition(Vector3 position) {
            float angleRad = MathFG.DirToAng(position - transform.position);
            angleRad += Time.deltaTime * speed * -rotation;

            return transform.position + (Vector3)(MathFG.AngToDir(angleRad) * trajectoryRadius);
        }
    }

}