using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.Gameplay.UI {

    [RequireComponent(typeof(UIDocument))]
    public class GameplayUI : MonoBehaviour {

        public Action clickedPause;

        VisualElement root;
        TextElement level;
        Button pauseButton;

        public string LevelName {
            set => level.text = value;
        }

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;

            root = rootEl.Q<VisualElement>("Root");
            level = rootEl.Q<TextElement>("Level");
            pauseButton = rootEl.Q<Button>("PauseButton");
        }

        void OnEnable() {
            pauseButton.clicked += OnPause;
        }

        void OnDisable() {
            pauseButton.clicked -= OnPause;
        }

        void OnPause() {
            clickedPause?.Invoke();
        }

    }
}