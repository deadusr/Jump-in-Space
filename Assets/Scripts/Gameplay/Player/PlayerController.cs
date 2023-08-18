using System;
using JumpInSpace.Gameplay.GameplayObjects;
using UnityEngine;

namespace JumpInSpace.Gameplay.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        Rocket rocket;

        [SerializeField]
        FinishLine finishLine;

        [SerializeField]
        Collider2D playAreaCollider;

        void OnEnable() {
            rocket.rocketCrushed += OnRocketCrush;
            finishLine.finished += OnFinish;    
        }

        void OnDisable() {
            rocket.rocketCrushed -= OnRocketCrush;
            finishLine.finished -= OnFinish;
        }

        void FixedUpdate() {
            if (playAreaCollider && !playAreaCollider.bounds.Contains(rocket.transform.position)) {
                OnPlayerOutOfBounds();
            }
        }

        void OnRocketCrush() {
            PlayerManager.Instance.LoseLevel("Your rocket blow up");
            rocket.Stop();
            rocket.BlowUp();
        }

        void OnPlayerOutOfBounds() {
            rocket.Stop();
            PlayerManager.Instance.LoseLevel("You've lost control of your ship");
        }

        void OnFinish() {
            PlayerManager.Instance.WinLevel();
            rocket.Land(finishLine.transform.position);
        }
    }
}