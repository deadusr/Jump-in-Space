using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace JumpInSpace.Utils {
    public class InputController : Singleton<InputController> {
        public Action Interact;
        public Action InteractEnd;
        public Action AltInteract;
        public Action AltInteractEnd;
        public Action<Vector2> UIClick;
        public Action UIClickEnd;
        
        InputControls controls;

        void Start() {
            Initialize();
        }

        void Initialize() {
            controls = new InputControls();
            controls.Default.Enable();
            controls.UI.Enable();
            controls.Default.Interact.performed += OnInteract;
            controls.Default.Interact.canceled += OnInteractEnd;
            controls.Default.AltAction.performed += OnAltInteract;
            controls.Default.AltAction.canceled += OnAltInteractEnd;
            controls.UI.Click.performed += OnUICLick;
            controls.UI.Click.canceled += OnUICLickEnd;
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

        void OnUICLick(InputAction.CallbackContext context) {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }

            Vector2 currentPosition = controls.UI.Point.ReadValue<Vector2>();
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
            UIClick?.Invoke(currentPosition);
        }

        void OnUICLickEnd(InputAction.CallbackContext context) {
            Debug.Log("OnUICLickEnd");
            UIClickEnd?.Invoke();   
        }
    }

}