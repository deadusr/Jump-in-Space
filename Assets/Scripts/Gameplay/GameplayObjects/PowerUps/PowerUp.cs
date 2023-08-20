using System;
using JumpInSpace.Gameplay.Player;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects.PowerUps {
    public abstract class PowerUp : MonoBehaviour {
        public Action<PowerUp> trigger;
        public abstract void ApplyPowerUp(PowerUpsController powerUpsController);
        public abstract void EndPowerUp(PowerUpsController powerUpsController);
        public abstract float PowerUpDuration { get; }
    }
}