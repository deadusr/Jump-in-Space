﻿using System;
using JumpInSpace.Gameplay.UI.UIPanel;
using UnityEngine;
using JumpInSpace.UnityServices;
using UnityEngine.SceneManagement;

namespace JumpInSpace.Gameplay.UI.MainMenu {
    public class MainMenuUIController : MonoBehaviour {
        [SerializeField]
        MainMenuUI mainMenuUI;

        [SerializeField]
        UIPanelController uiPanelController;

        void Start() {
            if (!AccountManagerService.IsSignIn)
                uiPanelController.ShowAuthPanel();
        }

        void OnEnable() {
            mainMenuUI.clickLevelsButton += OnClickLevels;
            mainMenuUI.clickArenaButton += OnClickArena;
            mainMenuUI.clickEditorButton += OnClickEditor;
        }

        void OnDisable() {
            mainMenuUI.clickLevelsButton -= OnClickLevels;
            mainMenuUI.clickArenaButton -= OnClickArena;
            mainMenuUI.clickEditorButton -= OnClickEditor;
        }

        void OnClickLevels() {
            GameplayManager.Instance.SetGameMode(GameMode.Common);
            GameplayManager.Instance.StartGame();
        }
        
        void OnClickArena() {
            GameplayManager.Instance.SetGameMode(GameMode.Arena);
            GameplayManager.Instance.StartGame();
        }

        void OnClickEditor() {
            SceneManager.LoadScene("LevelEditor");
        }

        void Update() {
            if (AccountManagerService.Username != null && mainMenuUI.UserName != AccountManagerService.Username)
                mainMenuUI.UserName = AccountManagerService.Username;
        }
    }
}