using System;
using System.Collections;
using JumpInSpace.Gameplay.Economy;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.UnityServices;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.Player {
    public class PlayerController : MonoBehaviour {

        [SerializeField]
        CameraFollower cameraFollower;
        
        [SerializeField]
        Transform startLine;

        [SerializeField]
        FinishLine finishLine;

        [SerializeField]
        Collider2D playAreaCollider;

        Rocket rocket;
        float launchTime;

        public float TimePassed => launchTime != 0 ? Time.time - launchTime : 0;
        public Rocket Rocket => rocket;

        void OnEnable() {
            rocket.rocketCrushed += OnRocketCrush;
            rocket.rocketLaunched += OnRocketLaunched;
            finishLine.finished += OnFinish;
        }

        void OnDisable() {
            rocket.rocketCrushed -= OnRocketCrush;
            finishLine.finished -= OnFinish;
        }

        void Awake() {
            rocket = Instantiate(EconomyManager.Instance.ActiveRocket.RocketGO, startLine.position, Quaternion.Euler(0, 0, -45));
            rocket.SetupRocket(EconomyManager.Instance.ActiveRocket);
            cameraFollower.SetupFollowTransform(rocket.transform);
        }

        void FixedUpdate() {
            if (playAreaCollider && !playAreaCollider.bounds.Contains(rocket.transform.position)) {
                OnPlayerOutOfBounds();
            }
        }

        void OnRocketCrush() {
            PlayerManager.Instance.LoseLevel("Your rocket blow up");
            rocket.StopRocket();
            rocket.BlowUp();
        }

        void OnRocketLaunched() {
            launchTime = Time.time;
        }

        void OnPlayerOutOfBounds() {
            rocket.StopRocket();
            PlayerManager.Instance.LoseLevel("You've lost control of your ship");
            GameplayManager.Instance.StopGame();
        }

        void OnFinish() {
            Debug.Log("OnFinish");
            float finalTime = Time.time - launchTime;
            PlayerManager.Instance.WinLevel(finalTime);
            rocket.LandRocket(finishLine.transform.position);
            GameplayManager.Instance.StopGame();

            if (GameplayManager.Instance.Mode == GameMode.Arena)
                LeaderboardManager.SetScore(LevelManager.Instance.ActiveLevel.Id, finalTime);
            else {
                LevelManager.Instance.CompleteActiveLevel(finalTime);
            }
        }
    }
}