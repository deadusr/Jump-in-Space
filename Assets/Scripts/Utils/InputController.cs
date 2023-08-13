using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JumpInSpace.Utils {
    public class InputController : MonoBehaviour {

        public static InputController Instance { get; private set; }

        public Action Interact;

        InputControls controls;
        void Awake() {
            controls = new InputControls();
            controls.Default.Enable();

            controls.Default.Interact.performed += OnInteract;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        void OnInteract(InputAction.CallbackContext _) {
            Interact?.Invoke();
        }
    }

}