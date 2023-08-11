using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Planet : MonoBehaviour {
    [SerializeField]
    float planetRadius;

    [SerializeField]
    [Range(0, 10)]
    float gravityRadius;

    [SerializeField]
    PlanetName planetName;

    [SerializeField]
    [Range(1, 3)]
    float gravityAngularSpeed = 1f;

    Vector2 planetToRocket;

    public float PlanetRadius => planetRadius;
    public float GravityRadius => gravityRadius;
    public float GravityAngularSpeed => gravityAngularSpeed;


    void Start() {
        PlanetController.Instance.AddPlanet(this);
    }

    void OnDrawGizmos() {
#if UNITY_EDITOR
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, planetRadius, 1f);

        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, gravityRadius, 1f);
#endif
    }

    public bool InsideGravity(Vector2 objectPosition) {
        float distance = Vector2.Distance(transform.position, objectPosition);
        return distance < gravityRadius;
    }
}