using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Utils;

namespace JumpInSpace.Gameplay {

    public enum GameMode {
        Common,
        Arena
    }

    public class GameplayManager : Singleton<GameplayManager> {

        public Action pausedGame;
        public Action resumedGame;

        GameMode mode = GameMode.Common;

        public GameMode Mode => mode;

        public void StartGame() {
            switch (mode) {
                case GameMode.Common:
                    ShowStages();
                    break;
                case GameMode.Arena:
                    ShowArena();
                    break;
            }
        }

        public void ContinueGame() {
            Time.timeScale = 1f;
        }

        public void StopGame() {
            Time.timeScale = 0f;
        }


        public void ShowHomePage() {
            SceneManager.LoadScene("MainScreen");
        }

        public void ShowStages() {
            SceneManager.LoadScene("Stages");
        }

        public void ShowLevels() {
            switch (mode) {
                case GameMode.Common:
                    SceneManager.LoadScene("Levels");
                    break;
                case GameMode.Arena:
                    SceneManager.LoadScene("Arena");
                    break;
            }
        }

        void ShowArena() {
            SceneManager.LoadScene("Arena");
        }

        public void SetGameMode(GameMode newMode) {
            mode = newMode;
        }

        public void PauseGame() {
            Time.timeScale = 0f;
            OnPauseGame();
        }

        void OnPauseGame() {
            pausedGame?.Invoke();
        }

        public void ResumeGame() {
            Time.timeScale = 1f;
            OnResumeGame();
        }

        public void Replay() {
            ContinueGame();
            LevelManager.Instance.ReplayLevel();
        }

        public void NextLevel() {
            LevelManager.Instance.LoadNextLevelInStage();
            ContinueGame();
        }

        public void QuitGame() {
            Time.timeScale = 1f;
            Application.Quit();
        }

        void OnResumeGame() {
            resumedGame?.Invoke();
        }

    }
}