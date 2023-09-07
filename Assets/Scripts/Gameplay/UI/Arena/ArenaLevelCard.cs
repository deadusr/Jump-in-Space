using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Arena {

    public class ArenaLevelCard {

        static readonly VisualTreeAsset levelCardAsset = Resources.Load<VisualTreeAsset>("UI/Components/ArenaLevelCard");

        public Action clicked;

        VisualElement rootEl;
        Button container;
        Label levelName;

        public VisualElement Initialize(string levelName) {

            rootEl = levelCardAsset.Instantiate();
            container = rootEl.Q<Button>("ArenaLevelCard");
            this.levelName = rootEl.Q<Label>("LevelName");

            SetupEvents();
            SetupData(levelName);

            return rootEl;
        }


        void SetupEvents() {
            container.clicked += () => {
                clicked?.Invoke();
            };
        }

        void SetupData(string levelName) {
            this.levelName.text = levelName;
        }
    }
}