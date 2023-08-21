using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;
using JumpInSpace.Gameplay.GameplayObjects;

namespace JumpInSpace.Gameplay.UI {

    [RequireComponent(typeof(UIDocument))]
    public class GameplayUI : MonoBehaviour {

        public Action clickedPause;

        VisualElement root;
        TextElement level;
        Button pauseButton;
        ProgressBar fuelLevel;
        VisualElement progressBarBackground;

        Rocket rocket;

        public string LevelName {
            set => level.text = value;
        }

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();
            rocket = FindObjectOfType<Rocket>();

            var rootEl = UIDocument.rootVisualElement;

            root = rootEl.Q<VisualElement>("Root");
            level = rootEl.Q<TextElement>("Level");
            pauseButton = rootEl.Q<Button>("PauseButton");
            fuelLevel = rootEl.Q<ProgressBar>("FuelLevel");
            progressBarBackground = rootEl.Q<VisualElement>(className: "unity-progress-bar__progress");
        }

        void OnEnable() {
            pauseButton.clicked += OnPause;
        }

        void OnDisable() {
            pauseButton.clicked -= OnPause;
        }

        void Update() {
            fuelLevel.value = rocket.FuelLevel;
            if (rocket.FuelLevel < 30f) {
                progressBarBackground.style.backgroundColor = new StyleColor(Color.red);
            }
            else {
                progressBarBackground.style.backgroundColor = new StyleColor(Color.green);
            }
        }

        void OnPause() {
            clickedPause?.Invoke();
        }

    }
}