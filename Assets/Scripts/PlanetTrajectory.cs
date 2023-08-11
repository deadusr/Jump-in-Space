using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Utils;

public class PlanetTrajectory : MonoBehaviour {

    [SerializeField]
    Transform planet;

    [SerializeField]
    float speed = 7f;

    [SerializeField]
    [Range(0, 10)]
    float trajectoryRadius = 2f;

    [SerializeField]
    [Range(-1, 1)]
    int rotation = -1;

    float angleRad;

    void OnDrawGizmos() {
#if UNITY_EDITOR
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(transform.position, Vector3.forward, trajectoryRadius);
#endif
    }


    void Start() {
        angleRad = MathFG.DirToAng(planet.position - transform.position);
    }

    void Update() {
        angleRad += Time.deltaTime * speed * -rotation;
        planet.position = transform.position + (Vector3)(MathFG.AngToDir(angleRad) * trajectoryRadius);
    }
}