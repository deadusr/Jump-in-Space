using System;
using JumpInSpace.Gameplay;
using UnityEngine;

namespace JumpInSpace.Levels.UI {
    public class LevelEditorUIController : MonoBehaviour {
        [SerializeField]
        LevelEditorUI levelEditorUI;
        [SerializeField]
        AddObjectsPanel addObjectsPanel;
        [SerializeField]
        ObjectPropertiesPanel objectPropertiesPanel;

        [SerializeField]
        LevelEditorController levelEditorController;

        [SerializeField]
        GameplayData gameplayData;

        LevelDataObject selectedLevelDataObject;

        void OnEnable() {
            levelEditorUI.showAddPanelClicked += OnShowAddPanelClicked;
            addObjectsPanel.addGameplayObject += OnAddObject;
            objectPropertiesPanel.changedPositionX += OnChangedPositionX;
            objectPropertiesPanel.changedPositionY += OnChangedPositionY;
            objectPropertiesPanel.changedRotation += OnChangedRotation;
            objectPropertiesPanel.changedScale += OnChangedScale;
            levelEditorController.levelObjectChanged += OnLevelObjectChanged;
        }

        void OnDisable() {
            levelEditorUI.showAddPanelClicked -= OnShowAddPanelClicked;
            addObjectsPanel.addGameplayObject -= OnAddObject;
            objectPropertiesPanel.changedPositionX -= OnChangedPositionX;
            objectPropertiesPanel.changedPositionY -= OnChangedPositionY;
            objectPropertiesPanel.changedRotation -= OnChangedRotation;
            objectPropertiesPanel.changedScale -= OnChangedScale;
            levelEditorController.levelObjectChanged -= OnLevelObjectChanged;
        }

        void Start() {
            addObjectsPanel.HidePanel();
            objectPropertiesPanel.HidePanel();
            addObjectsPanel.GameplayObjects = gameplayData.GameplayObjects;
        }

        void OnAddObject(GameplayObject gameplayObject) {
            levelEditorController.AddObject(gameplayObject);
            addObjectsPanel.HidePanel();
        }

        void OnShowAddPanelClicked() {
            addObjectsPanel.ShowPanel();
        }

        void OnChangedPositionX(float positionX) {
            levelEditorController.SetPosition(selectedLevelDataObject.Id, new Vector2(positionX, selectedLevelDataObject.Position.y));
        }

        void OnChangedPositionY(float positionY) {
            levelEditorController.SetPosition(selectedLevelDataObject.Id, new Vector2(selectedLevelDataObject.Position.x, positionY));
        }

        void OnChangedRotation(float newRotation) {
            levelEditorController.SetRotation(selectedLevelDataObject.Id, newRotation);
        }

        void OnChangedScale(Vector2 scale) {
            levelEditorController.SetScale(selectedLevelDataObject.Id, scale);
        }

        void OnLevelObjectChanged(string levelObjectId, LevelDataObject levelDataObject) {
            if (selectedLevelDataObject != null && selectedLevelDataObject.Id == levelObjectId)
                objectPropertiesPanel.LevelDataObject = levelDataObject;
        }

        public void SetSelectedLevelDataObject(LevelDataObject levelDataObject) {
            if (levelDataObject == null) {
                objectPropertiesPanel.HidePanel();
                return;
            }

            selectedLevelDataObject = levelDataObject;
            objectPropertiesPanel.LevelDataObject = levelDataObject;
            objectPropertiesPanel.ShowPanel();
        }
    }
}