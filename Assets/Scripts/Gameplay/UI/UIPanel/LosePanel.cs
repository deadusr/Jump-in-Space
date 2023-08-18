using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.UIPanel {

    [RequireComponent(typeof(UIDocument))]
    public class LosePanel : MonoBehaviour, IUIPanel {
        public Action clickReplay;

        VisualElement root;
        Label loseReason;
        Button replayButton;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            root = rootEl.Q("Root");
            loseReason = rootEl.Q<Label>("LoseReason");
            replayButton = rootEl.Q<Button>("ReplayButton");
        }

        void OnEnable() {
            replayButton.clicked += OnReplay;
        }
        void OnDisable() {
            replayButton.clicked -= OnReplay;
        }

        void Start() {
            Hide();
        }

        public string Reason {
            set => loseReason.text = value;
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }

        void OnReplay() {
            clickReplay?.Invoke();
        }
    }
}