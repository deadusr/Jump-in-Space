using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.Gameplay.UI.UIPanel {
    public class LevelFinishedPanel : MonoBehaviour, IUIPanel {
        Button nextLevelButton;
        TextElement levelInfo;
        VisualElement root;

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;

            nextLevelButton = rootEl.Q<Button>("NextLevelButton");
            levelInfo = rootEl.Q<TextElement>("LevelInfo");
            root = rootEl.Q<VisualElement>("Root");

            nextLevelButton.clicked += OnClickNextLevelButton;
            
            Hide();
        }

        void OnClickNextLevelButton() {
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;

        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }
    }
}