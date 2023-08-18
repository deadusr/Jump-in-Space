using System;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.MainMenu {
    public class MainMenuUIController : MonoBehaviour {
        [SerializeField]
        MainMenuUI mainMenuUI;

        void OnEnable() {
            mainMenuUI.clickPlayButton += OnClickPlay;
        }

        void OnDisable() {
            mainMenuUI.clickPlayButton -= OnClickPlay;
        }

        void OnClickPlay() {
            GameplayManager.Instance.ShowLevels();
        }
    }
}