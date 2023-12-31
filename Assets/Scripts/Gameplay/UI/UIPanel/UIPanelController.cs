﻿using System;
using JumpInSpace.Gameplay.Economy;
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
            authPanel.skipAuth += OnSkipAuth;
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
            authPanel.skipAuth -= OnSkipAuth;
        }

        public void ShowAuthPanel() {
            ShowPanel(authPanel);
        }

        async void OnCreateAccount(string username, string password) {
            try {
                await AccountManagerService.SignUpWithUsernamePasswordAsync(username, password);
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
                await AccountManagerService.SignInWithUsernamePasswordAsync(username, password);
                authPanel.Hide();
            }
            catch (RequestFailedException ex) {
                authPanel.ShowErrorMessage(ex.Message);
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

        async void OnSkipAuth() {
            try {
                await AccountManagerService.SignInAnonymouslyAsync();
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

        void OnWinLevel(float completingTime) {
            bool hasNextLevel = LevelManager.Instance.HasNextLevelInStage;
            levelFinishedPanel.ShowNextLevelButton = hasNextLevel;
            levelFinishedPanel.ShowLoadLevelsButton = hasNextLevel;
            levelFinishedPanel.ShowLoadStagesButton = !hasNextLevel;
            levelFinishedPanel.LevelFinishedTime = TimeFormat.Format(completingTime);
            levelFinishedPanel.WinMoney = LevelManager.Instance.IsActiveLevelAlreadyCompleted || completingTime > LevelManager.Instance.ActiveLevel.TimeToComplete ? "0" : LevelManager.Instance.ActiveLevel.MoneyForCompleting.ToString();
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