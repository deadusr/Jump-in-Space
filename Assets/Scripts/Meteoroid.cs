using System;
using UnityEngine;

public class Meteoroid : MonoBehaviour {
    
    [SerializeField]
    float speed = 0;

    [SerializeField]
    GameplayUI gameplayUI;

    [SerializeField]
    Path followPath;

    void Start() {
        if (followPath) {
            followPath.FollowPath(transform, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out Rocket launcher)) {
            launcher.BlowUp();
            gameplayUI.ShowLosePanel("BOOM!!! Your rocket is blown");
        }
    }
}