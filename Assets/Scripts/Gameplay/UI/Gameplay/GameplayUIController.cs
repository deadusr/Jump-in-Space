using System;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Gameplay.Player;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI {
    public class GameplayUIController : MonoBehaviour {
        [SerializeField]
        GameplayUI gameplayUI;
        [SerializeField]
        PlayerController playerController;

        void Start() {
            gameplayUI.LevelName = LevelManager.Instance.ActiveLevel.LevelName;
        }

        void OnEnable() {
            gameplayUI.clickedPause += OnPause;
        }

        void OnDisable() {
            gameplayUI.clickedPause -= OnPause;
        }

        void Update() {
            gameplayUI.TimePassedOnLevel = playerController.TimePassed;
        }


        void OnPause() {
            GameplayManager.Instance.PauseGame();
        }
    }
}