using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Levels.UI {
    public class GameplayObjectCard {

        static readonly VisualTreeAsset cardAsset = Resources.Load<VisualTreeAsset>("UI/Components/GameplayObjectCard");

        public Action clicked;

        VisualElement rootEl;
        Button container;
        Label objectName;

        public string ObjectName {
            set => objectName.text = value;
        }

        public VisualElement Initialize() {

            rootEl = cardAsset.Instantiate();
            container = rootEl.Q<Button>("GameplayObjectCard");
            objectName = rootEl.Q<Label>("ObjectName");

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