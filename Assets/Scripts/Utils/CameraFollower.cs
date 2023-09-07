using System;
using UnityEngine;

namespace JumpInSpace.Utils {

    public enum CameraMode {
        Follow,
        Boundaries
    }

    public class CameraFollower : MonoBehaviour {
        [SerializeField]
        Transform followTf;

        [SerializeField]
        BoxCollider2D playArea;

        [SerializeField]
        float movementSpeed = 1f;

        [SerializeField]
        float screenLeftBoundaryPercent;

        [SerializeField]
        float screenRightBoundaryPercent;

        [SerializeField]
        CameraMode mode;

        Camera camera;

        bool isCameraMoving;
        Vector3 targetPosition;
        Vector3 startPosition;
        float t;


        void Start() {
            camera = GetComponent<Camera>();
        }

        void Update() {

            if (mode == CameraMode.Follow) {
                float camHalfHeight = camera.orthographicSize;
                float camHalfWidth = camera.aspect * camHalfHeight;

                float minXPos = playArea.bounds.min.x + camHalfWidth;
                float maxXPos = playArea.bounds.max.x - camHalfWidth;
                float x = Mathf.Clamp(followTf.position.x, minXPos, maxXPos);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
            else {
                if (isCameraMoving) {
                    t += Time.deltaTime * movementSpeed;
                    transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                    if (transform.position == targetPosition) {
                        isCameraMoving = false;
                    }
                }
                else if (OutsideBoundaries()) {
                    float camHalfHeight = camera.orthographicSize;
                    float camHalfWidth = camera.aspect * camHalfHeight;

                    float minXPos = playArea.bounds.min.x + camHalfWidth;
                    float maxXPos = playArea.bounds.max.x - camHalfWidth;
                    float minYPos = playArea.bounds.min.y + camHalfHeight;
                    float maxYPos = playArea.bounds.max.y - camHalfHeight;

                    float camShiftFromCenter = camHalfWidth * 0.10f; // shift on 5% to the right
                    float xDirection = followTf.position.x - transform.position.x > 0 ? 1 : -1;
                    // float yDirection = followTf.position.y - transform.position.y > 0 ? 1 : -1;

                    float x = Mathf.Clamp(followTf.position.x + (camShiftFromCenter * xDirection), minXPos, maxXPos);
                    // float y = Mathf.Clamp(followTf.position.y + (camShiftFromCenter * yDirection), minYPos, maxYPos);
                    targetPosition = new Vector3(x, transform.position.y, transform.position.z);
                    startPosition = transform.position;
                    isCameraMoving = true;
                    t = 0f;
                }
            }

        }



        bool OutsideBoundaries() {
            Vector2 position = followTf.position;

            Vector2 positionInViewport = camera.WorldToViewportPoint(position);
            bool horizontal = positionInViewport.x < (screenLeftBoundaryPercent * 0.01f) || positionInViewport.x > (screenRightBoundaryPercent * 0.01f);
            // bool vertical = positionInViewport.y < (screenLeftBoundaryPercent * 0.01f) || positionInViewport.y > (screenRightBoundaryPercent * 0.01f);
            return horizontal;
        }
    }
}