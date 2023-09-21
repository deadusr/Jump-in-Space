using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Levels {

    [RequireComponent(typeof(UIDocument))]
    public class LevelsUI : MonoBehaviour {

        public Action<Level> selectLevel;
        public Action goBack;

        List<Level> levels;

        VisualElement rootEl;
        VisualElement levelsContainer;
        Button backButton;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            rootEl = UIDocument.rootVisualElement;
            levelsContainer = rootEl.Q("LevelsContainer");
            backButton = rootEl.Q<Button>("BackButton");
        }

        void OnEnable() {
            backButton.clicked += OnClickBackButton;
        }

        void OnDisable() {
            backButton.clicked -= OnClickBackButton;
        }

        public void LoadLevels(List<Level> levels) {
            this.levels = levels;
            RenderLevels();
        }


        void OnSelectLevel(Level level) {
            selectLevel?.Invoke(level);
        }
        void OnClickBackButton() {
            goBack?.Invoke();
        }

        void RenderLevels() {
            levelsContainer.Clear();
            foreach (var level in levels) {
                var card = new LevelCard();
                card.clicked += () => {
                    OnSelectLevel(level);
                };
                levelsContainer.Add(card.Initialize());
                card.LevelName = level.LevelName;
                card.Completed = LevelManager.Instance.IsLevelCompleted(level);
            }
        }
    }
}