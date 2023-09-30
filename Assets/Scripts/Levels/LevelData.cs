using System;
using System.Collections.Generic;
using UnityEngine;

namespace JumpInSpace.Levels {
    public class LevelDataObject {
        public string Id;
        public string GameplayObjectId;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public List<object> Attributes;

        public LevelDataObject(
            string id,
            string gameplayObjectId,
            Vector2 position,
            float rotation,
            Vector2 scale) {
            Id = id;
            GameplayObjectId = gameplayObjectId;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }

    public class LevelData {
        public string Id;
        public string Name;
        public Dictionary<string, LevelDataObject> Objects = new Dictionary<string, LevelDataObject>();

        public void SetPosition(string levelObjectId, Vector2 position) {
            Objects[levelObjectId].Position = position;
        }
        
        public void SetRotation(string levelObjectId, float rotation) {
            Objects[levelObjectId].Rotation = rotation;
        }
        public void SetScale(string levelObjectId, Vector2 scale) {
            Objects[levelObjectId].Scale = scale;
        }
        
        public void AddObject(string objectId) {
            string id = Guid.NewGuid().ToString();

            Objects.Add(id,
                new LevelDataObject(
                    id,
                    objectId,
                    Vector2.zero,
                    0f,
                    Vector2.one
                )
            );

        }

        public void RemoveObject(string levelObjectId) {
            Objects.Remove(levelObjectId);
        }
    }
}