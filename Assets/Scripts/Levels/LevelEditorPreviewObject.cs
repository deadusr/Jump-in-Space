using System;
using System.Linq;
using JumpInSpace.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Levels {
    public class LevelEditorPreviewObject : MonoBehaviour {
        public Action<string, Vector2> objectMoved;
        string levelPreviewObjectId;

        BoxCollider2D collider2D;
        SpriteRenderer spriteRenderer;
        public string LevelPreviewObjectId => levelPreviewObjectId;

        float distance;
        bool isDragging;

        void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2D = GetComponent<BoxCollider2D>();
        }

        void OnEnable() {
            InputController.Instance.UIClick += OnClick;
            InputController.Instance.UIClickEnd += OnClickEnd;
        }

        public void InitObject(string levelPreviewObjectId) {
            this.levelPreviewObjectId = levelPreviewObjectId;

            SetSpriteSize(new Vector2(5, 5));
        }

        void Update() {

            if (isDragging) {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 rayPoint = ray.GetPoint(distance);

                transform.position = rayPoint;
            }

        }

        void OnMoved(Vector2 position) {
            objectMoved?.Invoke(levelPreviewObjectId, position);
        }

        public void UpdateProperties(Vector2 position, float rotation, Vector2 scale) {
            var tf = transform;
            tf.position = position;
            tf.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            tf.localScale = scale;

            SetSpriteSize(new Vector2(5, 5));
        }


        void SetSpriteSize(Vector2 size) {
            spriteRenderer.size = size;
            collider2D.size = size;
        }


        void OnClick(Vector2 screenPosition) {
            if (!Camera.main)
                return;

            float maxDistance = 100f;
            var ray = Camera.main.ScreenPointToRay(screenPosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, maxDistance);
            bool clickedOnObj = hits.Any(el => el.transform.GetInstanceID() == transform.GetInstanceID());

            if (clickedOnObj) {
                isDragging = true;
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            }
            else {
                isDragging = false;
            }
        }

        void OnClickEnd() {
            isDragging = false;
            OnMoved(transform.position);
        }
    }
}