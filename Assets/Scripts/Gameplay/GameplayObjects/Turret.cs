using System;
using System.Collections;
using UnityEngine;
using JumpInSpace.Utils;
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

        [SerializeField]
        float timeToReload = 3f;

        bool isReloading = false;

        void OnDrawGizmos() {
            float angleRad = angle * Mathf.Deg2Rad;
            Gizmos.color = Handles.color = IsRocketInsideFOW() ? Color.red : Color.green;

            Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
            Handles.DrawWireArc(Vector3.zero, Vector3.forward, Vector2.up, -angle / 2, radius);
            Handles.DrawWireArc(Vector3.zero, Vector3.forward, Vector2.up, angle / 2, radius);
            Gizmos.matrix = Handles.matrix = Matrix4x4.identity;

        }


        void FixedUpdate() {
            if (!isReloading && IsRocketInsideFOW()) {
                ShootMissile();
            }
        }

        void ShootMissile() {
            var m = Instantiate(missile, transform.position, transform.rotation);
            m.Launch(transform.position, (rocket.transform.position - transform.position).normalized, missileSpeed);
            StartCoroutine(Reload());
        }


        IEnumerator Reload() {
            isReloading = true;
            yield return new WaitForSeconds(timeToReload);
            isReloading = false;
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