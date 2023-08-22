using System;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Gameplay.Player;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.UIPanel {

    public class UIPanelController : MonoBehaviour {
        [SerializeField]
        LosePanel losePanel;
        [SerializeField]
        PausePanel pausePanel;
        [SerializeField]
        LevelFinishedPanel levelFinishedPanel;

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
        }


        void OnLoseLevel(string reason) {
            losePanel.Reason = reason;
            ShowPanel(losePanel);
        }

        void OnWinLevel() {
            ShowPanel(levelFinishedPanel);
        }

        void OnPausedGame() {
            ShowPanel(pausePanel);
        }

        void OnResumeGame() {
            HideActivePanel();
        }

        void OnReplay() {
            LevelManager.Instance.ReplayLevel();
        }

        void OnContinue() {
            GameplayManager.Instance.ResumeGame();
        }

        void OnShowLevels() {
            GameplayManager.Instance.ShowLevels();
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