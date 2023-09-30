using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JumpInSpace.Gameplay;
using JumpInSpace.Levels.UI;
using JumpInSpace.Utils;

namespace JumpInSpace.Levels {
    public class LevelEditorPreviewController : MonoBehaviour {

        [SerializeField] 
        GameplayData gameplayData;
        [SerializeField] 
        LevelEditorController levelEditorController;
        [SerializeField] 
        LevelEditorUIController levelEditorUIController;
        [SerializeField]
        Sprite backgroundSprite;
        [SerializeField]
        LevelEditorPreviewObject levelEditorPreviewObject;

        Dictionary<string, LevelEditorPreviewObject> previewObjects = new Dictionary<string, LevelEditorPreviewObject>();

        void Awake() {
            InitLevelPreview(levelEditorController.ActiveLevel);
        }

        void OnEnable() {
            InputController.Instance.UIClick += OnClick;
            levelEditorController.activeLevelChanged += UpdatePreview;
        }

        void OnDisable() {
            levelEditorController.activeLevelChanged -= UpdatePreview;
            InputController.Instance.UIClick -= OnClick;
        }

        void InitLevelPreview(LevelData level) {
            foreach (var levelDataObject in level.Objects.Values) {
                AddPreviewObject(levelDataObject);
            }
        }

        void UpdatePreview(LevelData level) {

            var idsToUpdate = level.Objects.Keys.ToList().Intersect(previewObjects.Keys);
            var idsToAddToScene = level.Objects.Keys.ToList().Except(previewObjects.Keys);
            var idsToRemoveFromScene = previewObjects.Keys.ToList().Except(level.Objects.Keys);

            foreach (var id in idsToUpdate) {
                UpdatePreviewObject(level.Objects[id]);
            }

            foreach (var id in idsToAddToScene) {
                AddPreviewObject(level.Objects[id]);
            }

            foreach (var id in idsToRemoveFromScene) {
                RemovePreviewObject(id);
            }
        }

        void AddPreviewObject(LevelDataObject levelDataObject) {
            var gameplayObj = gameplayData.GetGameplayObject(levelDataObject.GameplayObjectId);
            var previewObj = Instantiate(levelEditorPreviewObject);
            
            previewObj.InitObject(levelDataObject.Id);
            previewObj.objectMoved += OnPreviewObjectMoved;
            
            var tf = Instantiate(
                gameplayObj.Prefab,
                levelDataObject.Position,
                Quaternion.Euler(levelDataObject.Rotation,0,0)
            );
            tf.localScale = levelDataObject.Scale;
            tf.parent = previewObj.transform;
            previewObjects.Add(previewObj.LevelPreviewObjectId, previewObj);
        }

        void RemovePreviewObject(string levelPreviewObjectId) {
            Destroy(previewObjects[levelPreviewObjectId]);
            previewObjects.Remove(levelPreviewObjectId);
        }

        void UpdatePreviewObject(LevelDataObject levelDataObject) {
            var previewObj = previewObjects[levelDataObject.Id];
            previewObj.UpdateProperties(levelDataObject.Position, levelDataObject.Rotation, levelDataObject.Scale);
        }

        void OnClickOnPreviewObject(string levelObjectId) {
            levelEditorUIController.SetSelectedLevelDataObject(
                levelEditorController.GetLevelObject(levelObjectId)
            );
        }

        void OnPreviewObjectMoved(string levelObjectId, Vector2 position) {
            levelEditorController.SetPosition(levelObjectId, position);
        }

        void OnClick(Vector2 screenPosition) {
            if (!Camera.main)
                return;
            
            float maxDistance = 100f;
            var ray = Camera.main.ScreenPointToRay(screenPosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, maxDistance);
     
            foreach (var hit in hits) {
                var previewObj = hit.transform.GetComponent<LevelEditorPreviewObject>();
                if (previewObj != null) {
                    OnClickOnPreviewObject(previewObj.LevelPreviewObjectId);
                    return;
                }
            }

            OnClickOnPreviewObject(null);
        }
    }
}