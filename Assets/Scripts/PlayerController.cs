using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay;
using UnityEngine;
using JumpInSpace.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    Transform rocket;

    [SerializeField]
    Transform playArea;

    [SerializeField]
    GameplayUI gameplayUI;

    Collider2D playAreaCollider;

    void Start() {
        playAreaCollider = playArea.GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        if (!playAreaCollider.bounds.Contains(rocket.position)) {
            Debug.Log("Player out of bounds");
            gameplayUI.ShowLosePanel("Player out of bounds");
            GameplayController.Instance.PauseGame();
        }
    }

}