using System;
using UnityEngine;
using JumpInSpace.UnityServices;
using Unity.Services.Core;
using Unity.Services.Authentication;
using JumpInSpace.Gameplay.Player;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Utils;

namespace JumpInSpace.Gameplay.UI.UIPanel {

    public class UIPanelController : MonoBehaviour {
        [SerializeField]
        LosePanel losePanel;
        [SerializeField]
        PausePanel pausePanel;
        [SerializeField]
        LevelFinishedPanel levelFinishedPanel;

        [SerializeField]
        AuthPanel authPanel;

        IUIPanel activePanel;

        void OnEnable() {
            PlayerManager.Instance.loseLevel += OnLoseLevel;
            PlayerManager.Instance.winLevel += OnWinLevel;
            GameplayManager.Instance.pausedGame += OnPausedGame;
            GameplayManager.Instance.resumedGame += OnResumeGame;

            losePanel.clickReplay += OnReplay;
            pausePanel.clickedContinue += OnContinue;
            pausePanel.quit += OnShowLevels;
            levelFinishedPanel.showLevels += OnShowLevels;
            levelFinishedPanel.showStages += OnShowStages;
            levelFinishedPanel.loadNextLevel += OnNextLevel;
            authPanel.createAccount += OnCreateAccount;
            authPanel.signIn += OnSignIn;
        }

        void OnDisable() {
            PlayerManager.Instance.loseLevel -= OnLoseLevel;
            PlayerManager.Instance.winLevel -= OnWinLevel;
            GameplayManager.Instance.pausedGame -= OnPausedGame;
            GameplayManager.Instance.resumedGame -= OnResumeGame;

            losePanel.clickReplay -= OnReplay;
            pausePanel.clickedContinue -= OnContinue;
            pausePanel.quit -= OnShowLevels;
            levelFinishedPanel.showLevels -= OnShowLevels;
            levelFinishedPanel.showStages -= OnShowStages;
            levelFinishedPanel.loadNextLevel -= OnNextLevel;
            authPanel.createAccount -= OnCreateAccount;
            authPanel.signIn -= OnSignIn;
        }

        public void ShowAuthPanel() {
            ShowPanel(authPanel);
        }

        async void OnCreateAccount(string username, string password) {
            try {
                await AccountManager.Instance.SignUpWithUsernamePasswordAsync(username, password);
                authPanel.Hide();
            }
            catch (RequestFailedException ex) {
                authPanel.ShowErrorMessage(ex.Message);
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

        async void OnSignIn(string username, string password) {
            try {
                await AccountManager.Instance.SignInWithUsernamePasswordAsync(username, password);
                authPanel.Hide();
            }
            catch (RequestFailedException ex) {
                authPanel.ShowErrorMessage(ex.Message);
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }


        void OnLoseLevel(string reason) {
            losePanel.Reason = reason;
            ShowPanel(losePanel);
        }

        void OnWinLevel(float time) {
            bool hasNextLevel = LevelManager.Instance.HasNextLevelInStage;
            levelFinishedPanel.ShowNextLevelButton = hasNextLevel;
            levelFinishedPanel.ShowLoadLevelsButton = hasNextLevel;
            levelFinishedPanel.ShowLoadStagesButton = !hasNextLevel;
            levelFinishedPanel.LevelFinishedTime = TimeFormat.Format(time);
            ShowPanel(levelFinishedPanel);
        }

        void OnPausedGame() {
            ShowPanel(pausePanel);
        }

        void OnResumeGame() {
            HideActivePanel();
        }

        void OnReplay() {
            GameplayManager.Instance.Replay();
        }

        void OnContinue() {
            GameplayManager.Instance.ResumeGame();
        }

        void OnShowLevels() {
            GameplayManager.Instance.ShowLevels();
        }

        void OnShowStages() {
            GameplayManager.Instance.ShowStages();
        }

        void OnNextLevel() {
            GameplayManager.Instance.NextLevel();
        }


        void ShowPanel(IUIPanel panel) {
            activePanel?.Hide();
            panel.Show();
            activePanel = panel;
        }

        void HideActivePanel() {
            activePanel?.Hide();
        }
    }
}