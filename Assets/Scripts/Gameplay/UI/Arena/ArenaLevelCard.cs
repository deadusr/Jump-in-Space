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
        Label userPosition;
        Label userTime;

        public VisualElement Initialize(string levelName, string playerPosition, string playerTime) {

            rootEl = levelCardAsset.Instantiate();
            container = rootEl.Q<Button>("ArenaLevelCard");
            this.levelName = rootEl.Q<Label>("LevelName");
            userPosition = rootEl.Q<Label>("UserPosition");
            userTime = rootEl.Q<Label>("UserTime");

            SetupEvents();
            SetupData(levelName, playerPosition, playerTime);

            return rootEl;
        }


        void SetupEvents() {
            container.clicked += () => {
                clicked?.Invoke();
            };
        }

        void SetupData(string levelName, string playerPosition, string playerTime) {
            this.levelName.text = levelName;
            userPosition.text = playerPosition;
            userTime.text = playerTime;
        }
    }
}