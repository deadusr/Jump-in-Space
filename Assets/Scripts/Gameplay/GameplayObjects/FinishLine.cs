using System;
using JumpInSpace.Gameplay;
using UnityEngine;


namespace JumpInSpace.Gameplay.GameplayObjects {
    public class FinishLine : MonoBehaviour {
        void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<Rocket>(out var launcher)) {
                launcher.Land(transform.position);
                FinishLevel();
            }
        }

        void FinishLevel() {
            GameplayController.Instance.FinishLevel();
        }
    }
}