using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Arena {

    [RequireComponent(typeof(UIDocument))]
    public class ArenaUI : MonoBehaviour {

        public Action<Level> selectLevel;

        List<Level> levels;

        VisualElement rootEl;
        VisualElement levelsContainer;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            rootEl = UIDocument.rootVisualElement;
            levelsContainer = rootEl.Q("LevelsContainer");
        }

        public void LoadLevels(List<Level> levels) {
            this.levels = levels;
            RenderLevels();
        }


        void OnSelectLevel(Level level) {
            selectLevel?.Invoke(level);
        }

        void RenderLevels() {
            levelsContainer.Clear();
            foreach (var level in levels) {
                var card = new ArenaLevelCard();
                card.clicked += () => {
                    OnSelectLevel(level);
                };
                levelsContainer.Add(card.Initialize(level.LevelName));
            }
        }
    }
}