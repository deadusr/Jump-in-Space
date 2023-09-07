using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace JumpInSpace.Utils {
    public class InputController : MonoBehaviour {
        public static InputController Instance { get; private set; }

        public Action Interact;
        public Action InteractEnd;
        public Action AltInteract;
        public Action AltInteractEnd;
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
            controls.Default.Interact.canceled += OnInteractEnd;
            controls.Default.AltAction.performed += OnAltInteract;
            controls.Default.AltAction.canceled += OnAltInteractEnd;
        }


        void OnInteract(InputAction.CallbackContext context) {
            if (!EventSystem.current.IsPointerOverGameObject())
                Interact?.Invoke();
        }
        void OnInteractEnd(InputAction.CallbackContext context) {
            if (!EventSystem.current.IsPointerOverGameObject())
                InteractEnd?.Invoke();
        }



        void OnAltInteract(InputAction.CallbackContext _) {
            AltInteract?.Invoke();
        }
        
        void OnAltInteractEnd(InputAction.CallbackContext _) {
            AltInteractEnd?.Invoke();
        }
    }

}