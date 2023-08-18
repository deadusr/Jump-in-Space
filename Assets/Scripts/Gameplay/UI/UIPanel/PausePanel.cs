using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.UIPanel {

    [RequireComponent(typeof(UIDocument))]
    public class PausePanel : MonoBehaviour, IUIPanel {
        public Action clickedContinue;

        VisualElement root;
        Button continueButton;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            root = rootEl.Q("Root");
            continueButton = rootEl.Q<Button>("ContinueButton");

            Hide();
        }

        void OnEnable() {
            continueButton.clicked += OnContinueClick;
        }

        void OnDisable() {
            continueButton.clicked -= OnContinueClick;
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
    }
}