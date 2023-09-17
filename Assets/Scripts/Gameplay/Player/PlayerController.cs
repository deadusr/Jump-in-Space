using System;
using System.Collections;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.UnityServices;
using UnityEngine;

namespace JumpInSpace.Gameplay.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        Rocket rocket;

        [SerializeField]
        FinishLine finishLine;

        [SerializeField]
        Collider2D playAreaCollider;


        float launchTime;

        public float TimePassed => launchTime != 0 ? Time.time - launchTime : 0;

        void OnEnable() {
            rocket.rocketCrushed += OnRocketCrush;
            rocket.rocketLaunched += OnRocketLaunched;
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

        void OnRocketLaunched() {
            launchTime = Time.time;
        }

        void OnPlayerOutOfBounds() {
            rocket.Stop();
            PlayerManager.Instance.LoseLevel("You've lost control of your ship");
            GameplayManager.Instance.StopGame();
        }

        void OnFinish() {
            float finalTime = Time.time - launchTime;
            PlayerManager.Instance.WinLevel(finalTime);
            rocket.Land(finishLine.transform.position);
            GameplayManager.Instance.StopGame();

            if (GameplayManager.Instance.Mode == GameMode.Arena)
                LeaderboardManager.Instance.SetScore(LevelManager.Instance.ActiveLevel.Id, finalTime);
        }
    }
}