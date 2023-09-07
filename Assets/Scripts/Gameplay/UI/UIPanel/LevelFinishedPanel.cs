using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.Gameplay.UI.UIPanel {
    public class LevelFinishedPanel : MonoBehaviour, IUIPanel {
        public Action showLevels;
        public Action showStages;
        public Action loadNextLevel;

        Button loadLevelsButton;
        Button nextLevelButton;
        Button loadStagesButton; 
        Label timeInfo;
        VisualElement root;

        public bool ShowNextLevelButton {
            set => nextLevelButton.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }
        
        public bool ShowLoadStagesButton {
            set => loadStagesButton.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }
        
        public bool ShowLoadLevelsButton {
            set => loadLevelsButton.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }
        
        public string LevelFinishedTime {
            set => timeInfo.text = value;
        }

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;

            loadLevelsButton = rootEl.Q<Button>("LoadLevelsButton");
            nextLevelButton = rootEl.Q<Button>("NextLevelButton");
            loadStagesButton = rootEl.Q<Button>("LoadStagesButton");
            timeInfo = rootEl.Q<Label>("Time");
            root = rootEl.Q<VisualElement>("Root");

            loadLevelsButton.clicked += OnShowLevels;
            loadStagesButton.clicked += OnShowStages;
            nextLevelButton.clicked += OnLoadNextLevel;

            Hide();
        }

        void OnShowLevels() {
            showLevels?.Invoke();
        }

        void OnShowStages() {
            showStages?.Invoke();
        }

        
        void OnLoadNextLevel() {
            loadNextLevel?.Invoke();
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;

        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }
    }

}