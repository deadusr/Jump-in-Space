using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    Transform rocket;
    
    [SerializeField]
    Collider2D playAreaCollider;


    void FixedUpdate() {
        if (playAreaCollider != null && !playAreaCollider.bounds.Contains(rocket.position)) {
            Debug.Log("Player out of bounds");
            GameplayController.Instance.LoseGame("Player out of bounds");
        }
    }

}