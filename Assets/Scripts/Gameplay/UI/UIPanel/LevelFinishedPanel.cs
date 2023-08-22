using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.Gameplay.UI.UIPanel {
    public class LevelFinishedPanel : MonoBehaviour, IUIPanel {
        public Action showLevels;
        
        Button loadLevelsButton;
        TextElement levelInfo;
        VisualElement root;

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;

            loadLevelsButton = rootEl.Q<Button>("LoadLevelsButton");
            levelInfo = rootEl.Q<TextElement>("LevelInfo");
            root = rootEl.Q<VisualElement>("Root");

            loadLevelsButton.clicked += OnShowLevels;
            
            Hide();
        }

        void OnShowLevels() {
            showLevels?.Invoke();
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;

        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }
    }
}