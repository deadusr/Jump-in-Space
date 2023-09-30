using System;
using System.Collections.Generic;
using System.Globalization;
using JumpInSpace.Gameplay;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Levels.UI {
    public class ObjectPropertiesPanel : MonoBehaviour {
        public Action<float> changedPositionX;
        public Action<float> changedPositionY;
        public Action<float> changedRotation;
        public Action<Vector2> changedScale;

        VisualElement objectPropertiesPanel;
        TextField positionX;
        TextField positionY;
        TextField rotationX;
        TextField scaleX;
        TextField scaleY;

        public LevelDataObject LevelDataObject {
            set {
                positionX.value = value.Position.x.ToString();
                positionY.value = value.Position.y.ToString();
                rotationX.value = value.Rotation.ToString();
                scaleX.value = value.Scale.x.ToString();
                scaleY.value = value.Scale.y.ToString();
            }
        }

        void Awake() {
            var UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement;
            objectPropertiesPanel = rootEl.Q<VisualElement>("ObjectPropertiesPanel");
            positionX = rootEl.Q<TextField>("PositionX");
            positionY = rootEl.Q<TextField>("PositionY");
            rotationX = rootEl.Q<TextField>("RotationX");
            scaleX = rootEl.Q<TextField>("ScaleX");
            scaleY = rootEl.Q<TextField>("ScaleY");
        }

        void OnEnable() {
            positionX.RegisterValueChangedCallback(OnChangePositionX);
            positionY.RegisterValueChangedCallback(OnChangePositionY);
            rotationX.RegisterValueChangedCallback(OnChangeRotationX);
            scaleX.RegisterValueChangedCallback(OnChangeScaleX);
            scaleY.RegisterValueChangedCallback(OnChangeScaleY);
        }

        void OnDisable() {
            positionX.UnregisterValueChangedCallback(OnChangePositionX);
            positionY.UnregisterValueChangedCallback(OnChangePositionY);
            rotationX.UnregisterValueChangedCallback(OnChangeRotationX);
            scaleX.UnregisterValueChangedCallback(OnChangeScaleX);
            scaleY.UnregisterValueChangedCallback(OnChangeScaleY);
        }


        public void HidePanel() {
            objectPropertiesPanel.style.display = DisplayStyle.None;
        }

        public void ShowPanel() {
            objectPropertiesPanel.style.display = DisplayStyle.Flex;
        }

        void OnChangePositionX(ChangeEvent<string> changeEvent) {
            OnChangePositionX(float.Parse(changeEvent.newValue));
        }
        void OnChangePositionY(ChangeEvent<string> changeEvent) {
            OnChangePositionY(float.Parse(changeEvent.newValue));
        }
        void OnChangeRotationX(ChangeEvent<string> changeEvent) {
            OnChangeRotation(float.Parse(changeEvent.newValue));
        }
        void OnChangeScaleX(ChangeEvent<string> changeEvent) {
            OnChangeScale(new Vector2(float.Parse(changeEvent.newValue), float.Parse(scaleY.value)));
        }

        void OnChangeScaleY(ChangeEvent<string> changeEvent) {
            OnChangeScale(new Vector2(float.Parse(scaleX.value), float.Parse(changeEvent.newValue)));
        }

        void OnChangePositionX(float positionX) {
            changedPositionX?.Invoke(positionX);
        }
        
        void OnChangePositionY(float positionY) {
            changedPositionY?.Invoke(positionY);
        }

        void OnChangeRotation(float rotation) {
            changedRotation?.Invoke(rotation);
        }

        void OnChangeScale(Vector2 scale) {
            changedScale?.Invoke(scale);
        }
    }

}