using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Stages {
    public class StageCard {

        static readonly VisualTreeAsset levelCardAsset = Resources.Load<VisualTreeAsset>("UI/Components/LevelCard");

        public Action clicked;

        VisualElement rootEl;
        Button container;
        Label levelName;

        public VisualElement Initialize(string stageName) {

            rootEl = levelCardAsset.Instantiate();
            container = rootEl.Q<Button>("LevelCard");
            this.levelName = rootEl.Q<Label>("LevelName");

            SetupEvents();
            SetupData(stageName);

            return rootEl;
        }


        void SetupEvents() {
            container.clicked += () => {
                clicked?.Invoke();
            };
        }

        void SetupData(string stageName) {
            this.levelName.text = stageName;
        }
    }
}