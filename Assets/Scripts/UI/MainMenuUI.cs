using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using JumpInSpace.Gameplay;

namespace JumpInSpace.UI {
    public class MainMenuUI : MonoBehaviour {

        Button playButton;

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            playButton = rootEl.Q<Button>("PlayButton");
            playButton.clicked += OnClickPlayButton;
        }

        void OnClickPlayButton() {
            GameplayController.Instance.StartGame();
        }
    }
    
}

