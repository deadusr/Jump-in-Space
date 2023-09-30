using System;
using JetBrains.Annotations;
using JumpInSpace.Gameplay;
using UnityEngine;

namespace JumpInSpace.Levels {
    public class LevelEditorController : MonoBehaviour {
        public Action<LevelData> activeLevelChanged;
        public Action<string, LevelDataObject> levelObjectChanged;

        LevelData activeLevelData = new LevelData();

        public LevelData ActiveLevel => activeLevelData;

        public void SetLevel(LevelData level) {
            activeLevelData = level;
            OnLevelChanged();
        }

        public void SetPosition(string levelObjectId, Vector2 position) {
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            activeLevelData.SetPosition(levelObjectId, position);
            OnLevelChanged();
            OnObjectChanged(levelObjectId);
        }

        public void SetRotation(string levelObjectId, float rotation) {
            activeLevelData.SetRotation(levelObjectId, rotation);
            OnLevelChanged();
            OnObjectChanged(levelObjectId);
        }

        public void SetScale(string levelObjectId, Vector2 scale) {
            activeLevelData.SetScale(levelObjectId, scale);
            OnLevelChanged();
            OnObjectChanged(levelObjectId);
        }

        public void AddObject(GameplayObject gameplayObject) {
            activeLevelData.AddObject(gameplayObject.Id);
            OnLevelChanged();
        }

        public void RemoveObject(string levelObjectId) {
            activeLevelData.RemoveObject(levelObjectId);
            OnLevelChanged();
        }

        [CanBeNull]
        public LevelDataObject GetLevelObject(string levelObjectId) {
            if (activeLevelData == null || levelObjectId == null)
                return null;
            if (activeLevelData.Objects.TryGetValue(levelObjectId, out var levelDataObject)) {
                return levelDataObject;
            }
            return null;
        }

        void OnLevelChanged() {
            activeLevelChanged?.Invoke(activeLevelData);
        }

        void OnObjectChanged(string levelObjectId) {
            levelObjectChanged?.Invoke(levelObjectId, activeLevelData.Objects[levelObjectId]);
        }
    }
}