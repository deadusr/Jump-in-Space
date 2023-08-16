using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JumpInSpace.Utils {
    public class InputController : MonoBehaviour {
        public static InputController Instance { get; private set; }

        public Action Interact;
        public Action AltInteract;
        InputControls controls;
        
        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            }
            else {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
        }

        void Initialize() {
            controls = new InputControls();
            controls.Default.Enable();
            controls.Default.Interact.performed += OnInteract;
            controls.Default.AltAction.performed += OnAltInteract;
        }


        void OnInteract(InputAction.CallbackContext _) {
            Interact?.Invoke();
        }
        
        void OnAltInteract(InputAction.CallbackContext _) {
            AltInteract?.Invoke();
        }
    }

}