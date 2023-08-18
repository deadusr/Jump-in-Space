using System;
using UnityEngine;
using JumpInSpace.SaveSystem;
using JumpInSpace.Gameplay.GameplayObjects;

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class SavePoint : MonoBehaviour {

        void OnTriggerEnter2D(Collider2D other) {
            if (other.transform.TryGetComponent(out Rocket rocket)) {
                Debug.Log("OnTriggerEnter2D");
                GameplayManager.Instance.SaveGame();
            }
        }
    }

}