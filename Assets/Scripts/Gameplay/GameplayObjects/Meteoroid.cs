using System;
using UnityEngine;
using JumpInSpace.UI;

public class Meteoroid : MonoBehaviour {

    [SerializeField]
    GameplayUI gameplayUI;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out Rocket launcher)) {
            launcher.BlowUp();
            gameplayUI.ShowLosePanel("BOOM!!! Your rocket is blown");
        }
    }
}