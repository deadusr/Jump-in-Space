using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay;
using JumpInSpace.Gameplay.UI.Levels;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Levels.UI {

    [RequireComponent(typeof(UIDocument))]
    public class LevelEditorUI : MonoBehaviour {
        public Action playClicked;
        public Action showAddPanelClicked;

        Button playButton;
        Button showAddPanelButton;

        void Awake() {
            var UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement;

            playButton = rootEl.Q<Button>("PlayButton");
            showAddPanelButton = rootEl.Q<Button>("ShowAddPanelButton");
        }

        void OnEnable() {
            playButton.clicked += OnPlay;
            showAddPanelButton.clicked += OnShowAddPanelButton;
        }

        void OnDisable() {
            playButton.clicked -= OnPlay;
            showAddPanelButton.clicked -= OnShowAddPanelButton;
        }

        void OnPlay() {
            playClicked?.Invoke();
        }

        void OnShowAddPanelButton() {
            showAddPanelClicked?.Invoke();
        }
    }
}