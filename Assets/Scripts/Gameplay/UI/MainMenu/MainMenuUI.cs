using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.MainMenu {

    [RequireComponent(typeof(UIDocument))]
    public class MainMenuUI : MonoBehaviour {

        public Action clickPlayButton;

        Button playButton;

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            playButton = rootEl.Q<Button>("PlayButton");
            playButton.clicked += OnClickPlayButton;
        }

        void OnClickPlayButton() {
            clickPlayButton?.Invoke();
        }
    }

}