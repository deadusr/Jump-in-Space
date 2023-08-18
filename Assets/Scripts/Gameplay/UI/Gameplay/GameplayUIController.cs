using System;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI {
    public class GameplayUIController : MonoBehaviour {
        [SerializeField]
        GameplayUI gameplayUI;

        void Start() {
            gameplayUI.LevelName = LevelManager.Instance.ActiveLevel.LevelName;
        }

        void OnEnable() {
            gameplayUI.clickedPause += OnPause;
        }

        void OnDisable() {
            gameplayUI.clickedPause -= OnPause;
        }


        void OnPause() {
            GameplayManager.Instance.PauseGame();
        }
    }
}