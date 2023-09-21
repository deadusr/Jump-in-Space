using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Levels {
    public class LevelCard {

        static readonly VisualTreeAsset levelCardAsset = Resources.Load<VisualTreeAsset>("UI/Components/LevelCard");

        public Action clicked;

        VisualElement rootEl;
        Button container;
        Label levelName;

        public string LevelName {
            set => levelName.text = value;
        }

        public bool Completed {
            set => levelName.text = value ? "Completed" : levelName.text;
        }

        public VisualElement Initialize() {

            rootEl = levelCardAsset.Instantiate();
            container = rootEl.Q<Button>("LevelCard");
            levelName = rootEl.Q<Label>("LevelName");

            SetupEvents();

            return rootEl;
        }


        void SetupEvents() {
            container.clicked += () => {
                clicked?.Invoke();
            };
        }
    }
}