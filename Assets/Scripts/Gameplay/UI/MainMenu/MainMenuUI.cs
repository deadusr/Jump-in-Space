using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.MainMenu {

    [RequireComponent(typeof(UIDocument))]
    public class MainMenuUI : MonoBehaviour {

        public Action clickLevelsButton;
        public Action clickArenaButton;

        Button levelsButton;
        Button arenaButton;
        Label username;

        public string UserName {
            get => username.text;
            set => username.text = value;
        }

        void Start() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            levelsButton = rootEl.Q<Button>("LevelsButton");
            arenaButton = rootEl.Q<Button>("ArenaButton");
            username = rootEl.Q<Label>("Username");


            levelsButton.clicked += OnClickLevelsButton;
            arenaButton.clicked += OnClickArenaButton;
        }

        void OnClickLevelsButton() {
            clickLevelsButton?.Invoke();
        }
        
        void OnClickArenaButton() {
            clickArenaButton?.Invoke();
        }
    }

}