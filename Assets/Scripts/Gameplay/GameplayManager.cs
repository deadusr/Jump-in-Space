using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.SaveSystem;
using JumpInSpace.Gameplay.UI;
using JumpInSpace.Utils;
using UnityEngine.Serialization;

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

        public void ShowStages() {
            SceneManager.LoadScene("Stages");
        }

        public void ShowLevels() {
            SceneManager.LoadScene("Levels");
        }

        public void ShowArena() {
            SceneManager.LoadScene("Arena");
        }

        public void SetGameMode(GameMode newMode) {
            mode = newMode;
        }

        void StartLevel() {
            LoadSavedGame();
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
        
        
        public void SaveGame() {
            SaveSystemController.Instance.Save();
        }

        public void QuitGame() {
            Time.timeScale = 1f;
            Application.Quit();
        }

        void OnResumeGame() {
            resumedGame?.Invoke();
        }


        void LoadSavedGame() {
            var save = SaveSystemController.Instance.LoadSave();
            if (save == null) {
                return;
            }

            foreach (var savable in FindObjectsOfType<Savable>()) {
                string id = savable.GetComponent<GenID>().Id;
                savable.ApplySave(save.Value.Positions[id], save.Value.Rotations[id]);
            }

        }


    }
}