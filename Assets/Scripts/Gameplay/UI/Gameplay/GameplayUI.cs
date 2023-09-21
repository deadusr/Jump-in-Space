using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay.GameplayObjects;
using JumpInSpace.Utils;

namespace JumpInSpace.Gameplay.UI {

    [RequireComponent(typeof(UIDocument))]
    public class GameplayUI : MonoBehaviour {

        public Action clickedPause;

        VisualElement root;
        TextElement level;
        Button pauseButton;
        ProgressBar fuelLevel;
        VisualElement progressBarBackground;
        TextElement fpsCounter;
        Label timeCounter;

        Rocket rocket;

        float dtime;

        public string LevelName {
            set => level.text = value;
        }

        public float TimePassedOnLevel {
            set => timeCounter.text = $"{TimeFormat.Format(value)}";
        }

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement;

            root = rootEl.Q<VisualElement>("Root");
            level = rootEl.Q<TextElement>("Level");
            pauseButton = rootEl.Q<Button>("PauseButton");
            fuelLevel = rootEl.Q<ProgressBar>("FuelLevel");
            fpsCounter = rootEl.Q<TextElement>("FpsCounter");
            timeCounter = rootEl.Q<Label>("TimeCounter");
            progressBarBackground = rootEl.Q<VisualElement>(className: "unity-progress-bar__progress");
        }

        void Start() {
            rocket = FindObjectOfType<Rocket>();
        }

        void OnEnable() {
            pauseButton.clicked += OnPause;
        }

        void OnDisable() {
            pauseButton.clicked -= OnPause;
        }

        void Update() {
            if (Time.deltaTime != 0) {
                dtime += (Time.deltaTime - dtime) * 0.1f;
                int fps = (int)(1.0f / dtime);
                fpsCounter.text = $"{fps}fps";
            }


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