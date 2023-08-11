using System;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    [SerializeField]
    Transform followTf;

    [SerializeField]
    BoxCollider2D playArea;

    [SerializeField]
    float movementSpeed = 1f;

    Camera camera;

    bool isCameraMoving;
    Vector3 targetPosition;
    Vector3 startPosition;
    float t;


    void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
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

            float camShiftFromCenter = camHalfWidth * 0.5f; // shift on 25% to the right
            float xDirection = followTf.position.x - transform.position.x > 0 ? 1 : -1;

            float x = Mathf.Clamp(followTf.position.x + (camShiftFromCenter * xDirection), minXPos, maxXPos);
            targetPosition = new Vector3(x, transform.position.y, transform.position.z);
            startPosition = transform.position;
            isCameraMoving = true;
            t = 0f;
        }
    }



    bool OutsideBoundaries() {
        Vector2 position = followTf.position;

        Vector2 positionInViewport = camera.WorldToViewportPoint(position);

        return positionInViewport.x is < 0.05f or > 0.8f;
    }
}