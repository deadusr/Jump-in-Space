using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.UI {
    public class WinUI : MonoBehaviour {

        Button quitButton;
        TextElement gameSessionInfo;
        VisualElement root;

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            root = rootEl.Q<VisualElement>("Root");
            quitButton = rootEl.Q<Button>("QuitButton");
            gameSessionInfo = rootEl.Q<TextElement>("GameSessionInfo");

            quitButton.clicked += OnClickQuitButton;
        }

        void OnClickQuitButton() {
            GameplayController.Instance.QuitGame();
        }

        public void ShowPanel() {
            root.style.display = DisplayStyle.Flex;

        }

        public void HidePanel() {
            root.style.display = DisplayStyle.None;
        }
    }
}

