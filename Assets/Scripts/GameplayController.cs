using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {
    public static GameplayController Instance;

    [SerializeField]
    public int LevelsCount = 1;

    public int CurrentLevel = 1;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        MakeInstance();
    }

    void MakeInstance() {
        if (Instance == null) {
            Instance = this;
        }
    }
    
    public bool HasNextLevel => CurrentLevel < LevelsCount;

    public void NextLevel() {
        Debug.Log($"Level {CurrentLevel} finished");
        if (HasNextLevel) {
            CurrentLevel += 1;
            StartLevel();
        }
    }

    public void StartGame() {
        Time.timeScale = 1f;
        StartLevel();
    }

    void StartLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene($"GameplayLevel{CurrentLevel}");
    }

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        Application.Quit();
    }


    public void ReplayGame() {
        StartGame();
    }

    public void WinGame() {
        Time.timeScale = 0f;
        Debug.Log("Player win!");
    }


    public void MenuButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}