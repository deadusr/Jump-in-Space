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
        public Action clickEditorButton;

        Button levelsButton;
        Button arenaButton;
        Button editorButton;
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
            editorButton = rootEl.Q<Button>("EditorButton");
            username = rootEl.Q<Label>("Username");


            levelsButton.clicked += OnClickLevelsButton;
            arenaButton.clicked += OnClickArenaButton;
            editorButton.clicked += OnClickEditorButton;
        }

        void OnClickLevelsButton() {
            clickLevelsButton?.Invoke();
        }
        
        void OnClickArenaButton() {
            clickArenaButton?.Invoke();
        }
        
        void OnClickEditorButton() {
            clickEditorButton?.Invoke();
        }
    }

}