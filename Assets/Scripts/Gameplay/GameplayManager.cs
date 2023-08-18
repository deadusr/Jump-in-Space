using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.SaveSystem;
using JumpInSpace.Gameplay.UI;
using JumpInSpace.Utils;
using UnityEngine.Serialization;

namespace JumpInSpace.Gameplay {
    public class GameplayManager : Singleton<GameplayManager> {

        public Action pausedGame;
        public Action resumedGame;

        public void StartGame() {
            Time.timeScale = 1f;
        }

        public void ShowLevels() {
            SceneManager.LoadScene("Levels");
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

        void OnResumeGame() {
            resumedGame?.Invoke();
        }


        public void SaveGame() {
            SaveSystemController.Instance.Save();
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

        public void QuitGame() {
            Time.timeScale = 1f;
            Application.Quit();
        }

        public void WinGame() {
            Time.timeScale = 0f;
            Debug.Log("Player win!");
        }
    }
}