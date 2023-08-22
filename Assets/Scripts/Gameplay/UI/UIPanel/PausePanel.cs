using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.UIPanel {

    [RequireComponent(typeof(UIDocument))]
    public class PausePanel : MonoBehaviour, IUIPanel {
        public Action clickedContinue;
        public Action quit;

        VisualElement root;
        Button continueButton;
        Button quitButton;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            root = rootEl.Q("Root");
            continueButton = rootEl.Q<Button>("ContinueButton");
            quitButton = rootEl.Q<Button>("QuitButton");

            Hide();
        }

        void OnEnable() {
            continueButton.clicked += OnContinueClick;
            quitButton.clicked += OnQuit;
        }

        void OnDisable() {
            continueButton.clicked -= OnContinueClick;
            quitButton.clicked -= OnQuit;
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }

        void OnContinueClick() {
            clickedContinue?.Invoke();
        }

        void OnQuit() {
            quit?.Invoke();
        }
    }
}