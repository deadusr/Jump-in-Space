using System;
using JumpInSpace.Gameplay.Player;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects.PowerUps {

    [RequireComponent(typeof(Rigidbody2D))]
    public class BoosterPowerUp : PowerUp {

        [SerializeField]
        float powerUpDuration = 3f;

        public override float PowerUpDuration => powerUpDuration;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.TryGetComponent(out Rocket _)) {
                Debug.Log("OnTriggerEnter2D");
                trigger?.Invoke(this);
            }
        }

        public override void ApplyPowerUp(PowerUpsController powerUpsController) {
            powerUpsController.Rocket.ApplyBoost(powerUpDuration);
        }

        public override void EndPowerUp(PowerUpsController powerUpsController) {
        }

    }
}