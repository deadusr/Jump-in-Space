using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JumpInSpace.Gameplay.GameplayObjects {
    public class Turret : MonoBehaviour {

        [SerializeField]
        Rocket rocket;

        [SerializeField]
        float radius = 3f;

        [SerializeField]
        Missile missile;

        [SerializeField]
        float missileSpeed = 10f;

        [SerializeField]
        float angle = 180f;
        
        SpriteRenderer spriteRenderer;

        [SerializeField]
        float timeToReload = 3f;

        bool isReloading = false;
        float t;

        void Awake() {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        void OnDrawGizmos() {
#if UNITY_EDITOR
            float angleRad = angle * Mathf.Deg2Rad;
            Gizmos.color = Handles.color = IsRocketInsideFOW() ? Color.red : Color.green;

            Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
            Handles.DrawWireArc(Vector3.zero, Vector3.forward, Vector2.up, -angle / 2, radius);
            Handles.DrawWireArc(Vector3.zero, Vector3.forward, Vector2.up, angle / 2, radius);
            Gizmos.matrix = Handles.matrix = Matrix4x4.identity;
#endif

        }


        void FixedUpdate() {
            if (!isReloading && IsRocketInsideFOW()) {
                ShootMissile();
            }
        }

        void Update() {
            t += Time.deltaTime;
            float colorT = t / timeToReload;
            spriteRenderer.color = Color.Lerp(Color.green, Color.red, colorT);
        }

        void ShootMissile() {
            var m = Instantiate(missile, transform.position, transform.rotation);
            m.Launch(transform.position, (rocket.transform.position - transform.position).normalized, missileSpeed);
            StartCoroutine(Reload());
        }

        public void BlowUp() {
            Destroy(gameObject);
        }


        IEnumerator Reload() {
            isReloading = true;
            yield return new WaitForSeconds(timeToReload);
            isReloading = false;
            t = 0;
        }
        bool IsRocketInsideFOW() {
            Vector2 rocketPos = rocket.transform.position;
            Vector2 turretPos = transform.position;

            Vector2 vecToRocket = rocketPos - turretPos;

            if (vecToRocket.magnitude > radius)
                return false;

            float b = Vector2.Dot(transform.up, vecToRocket.normalized);
            float c = 1;
            float angleBetweenTurretAndRocketRad = Mathf.Acos(b / c);

            return angleBetweenTurretAndRocketRad * Mathf.Rad2Deg < angle / 2;
        }
    }
}