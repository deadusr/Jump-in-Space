using System;
using System.Collections;
using System.Collections.Generic;
using JumpInSpace.Gameplay.GameplayObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.SaveSystem;
using JumpInSpace.Gameplay.UI;

namespace JumpInSpace.Gameplay {
    public class GameplayController : MonoBehaviour {
        public static GameplayController Instance { get; private set; }

        [SerializeField]
        LevelFinishedUI levelFinishedUI;

        [SerializeField]
        WinUI winUI;

        [SerializeField]
        GameplayUI gameplayUI;

        [SerializeField]
        int levelsCount = 1;

        int currentLevel = 1;

        public int CurrentLevel => currentLevel;
        public int LevelsCount => levelsCount;

        void Awake() {
            DontDestroyOnLoad(gameObject);
            if (Instance != null && Instance != this) {
                Destroy(this);
            }
            else {
                Instance = this;
            }
        }

        public bool HasNextLevel => currentLevel < levelsCount;

        public void NextLevel() {
            Debug.Log($"Level {currentLevel} finished");
            if (HasNextLevel) {
                currentLevel += 1;
                StartLevel();
            }
        }

        public void StartGame() {
            Time.timeScale = 1f;
            StartLevel();
        }

        void StartLevel() {
            LoadSavedGame();
            // StartCoroutine(LoadSceneAsync($"GameplayLevel{CurrentLevel}"));
        }


        IEnumerator LoadSceneAsync(string path) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone) {
                yield return null;
            }
            Debug.Log("Scene loaded");
            LoadSavedGame();
        }

        public void PauseGame() {
            Time.timeScale = 0f;
        }

        public void ResumeGame() {
            Time.timeScale = 1f;
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


        public void ReplayGame() {
            StartGame();
            gameplayUI.HideLosePanel();
        }

        public void WinGame() {
            Time.timeScale = 0f;
            Debug.Log("Player win!");
        }

        public void FinishLevel() {
            if (HasNextLevel) {
                levelFinishedUI.ShowPanel();
            }
            else {
                winUI.ShowPanel();
            }
        }

        public void LoseGame(string reason) {
            gameplayUI.ShowLosePanel(reason);
            PauseGame();
        }


        public void MenuButton() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}