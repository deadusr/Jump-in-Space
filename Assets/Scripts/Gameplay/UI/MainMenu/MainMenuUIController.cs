using System;
using JumpInSpace.Gameplay.UI.UIPanel;
using UnityEngine;
using JumpInSpace.UnityServices;

namespace JumpInSpace.Gameplay.UI.MainMenu {
    public class MainMenuUIController : MonoBehaviour {
        [SerializeField]
        MainMenuUI mainMenuUI;

        [SerializeField]
        UIPanelController uiPanelController;

        void Start() {
            if (!AccountManager.Instance.IsSignIn)
                uiPanelController.ShowAuthPanel();
        }

        void OnEnable() {
            mainMenuUI.clickLevelsButton += OnClickLevels;
            mainMenuUI.clickArenaButton += OnClickArena;
        }

        void OnDisable() {
            mainMenuUI.clickLevelsButton -= OnClickLevels;
            mainMenuUI.clickArenaButton -= OnClickArena;
        }

        void OnClickLevels() {
            GameplayManager.Instance.SetGameMode(GameMode.Common);
            GameplayManager.Instance.StartGame();
        }
        
        void OnClickArena() {
            GameplayManager.Instance.SetGameMode(GameMode.Arena);
            GameplayManager.Instance.StartGame();
        }

        void Update() {
            if (AccountManager.Instance.Username != null && mainMenuUI.UserName != AccountManager.Instance.Username)
                mainMenuUI.UserName = AccountManager.Instance.Username;
        }
    }
}