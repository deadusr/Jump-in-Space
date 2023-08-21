using System;
using JumpInSpace.Gameplay;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class Meteoroid : MonoBehaviour {

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.TryGetComponent(out Rocket rocket)) {
                if (!rocket.Boosted)
                    rocket.Crush();
            }
        }
    }
}