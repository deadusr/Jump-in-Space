using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.UIPanel {
    [RequireComponent(typeof(UIDocument))]
    public class AuthPanel : MonoBehaviour, IUIPanel {
        public Action<string, string> signIn;
        public Action<string, string> createAccount;

        VisualElement root;
        Button signInButton;
        Button creatAccButton;
        Label errorMessage;

        TextField username;
        TextField password;

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            var rootEl = UIDocument.rootVisualElement;
            root = rootEl.Q("Root");
            signInButton = rootEl.Q<Button>("SignInButton");
            creatAccButton = rootEl.Q<Button>("CreatAccButton");
            username = rootEl.Q<TextField>("Username");
            password = rootEl.Q<TextField>("Password");
            errorMessage = rootEl.Q<Label>("ErrorMessage");

            Hide();
        }

        void OnEnable() {
            username.RegisterValueChangedCallback(OnChangeField);
            signInButton.clicked += OnSignIn;
            creatAccButton.clicked += OnCreateAcc;
        }

        void OnDisable() {
            signInButton.clicked -= OnSignIn;
            creatAccButton.clicked -= OnCreateAcc;
            username.UnregisterValueChangedCallback(OnChangeField);
        }

        public void Show() {
            root.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            root.style.display = DisplayStyle.None;
        }

        public void ShowErrorMessage(string message) {
            errorMessage.text = message;
            errorMessage.style.display = DisplayStyle.Flex;
        }

        void OnChangeField(ChangeEvent<string> _) {
            HideErrorMessage();
        }


        void HideErrorMessage() {
            if(errorMessage.style.display == DisplayStyle.None)
                return;
            errorMessage.text = "";
            errorMessage.style.display = DisplayStyle.None;
        }

        void OnSignIn() {
            if (username.value != "" && password.value != "")
                signIn?.Invoke(username.value, password.value);
        }

        void OnCreateAcc() {
            if (username.value != "" && password.value != "")
                createAccount?.Invoke(username.value, password.value);
        }
    }
}