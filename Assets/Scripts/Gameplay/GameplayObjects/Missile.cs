using System;
using UnityEngine;

namespace JumpInSpace.Gameplay.GameplayObjects {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Missile : MonoBehaviour {

        Vector2 launchDirection;
        float launchSpeed;
        bool isLaunched;

        public void Launch(Vector2 startPosition, Vector2 direction, float speed) {
            launchDirection = direction;
            launchSpeed = speed;
            isLaunched = true;
            transform.up = direction;
            transform.position = startPosition;
        }

        void Update() {
            if (isLaunched) {
                transform.position += (Vector3)(launchDirection * launchSpeed * Time.deltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.TryGetComponent(out Rocket rocket)) {
                rocket.Crush();
                Destroy(gameObject);
            }
            
            if (other.gameObject.TryGetComponent(out Turret turret)) {
                turret.BlowUp();
                Destroy(gameObject);
            }
        }
    }
}