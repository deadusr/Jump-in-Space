using System;
using UnityEngine;


namespace JumpInSpace.Gameplay.GameplayObjects {
    public class FinishLine : MonoBehaviour {

        public Action finished;
        void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<Rocket>(out var launcher)) {
                OnFinish();
            }
        }

        void OnFinish() {
            finished?.Invoke();
        }
    }
}