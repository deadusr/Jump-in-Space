using System;
using UnityEngine;

namespace JumpInSpace.Utils {
    public class ObjectOrbitFollower : MonoBehaviour {
        [SerializeField] Transform followObj;

        [SerializeField]
        float speed;

        [SerializeField]
        [Range(-1, 1)]
        int rotation = -1;

        float distanceToObj;
        float angle;

        void Start() {
            Vector2 vecToObj = (transform.position - followObj.position);
            distanceToObj = vecToObj.magnitude;
            angle = MathFG.DirToAng(vecToObj);
            Debug.Log(angle);
        }

        void Update() {
            angle += speed * rotation * Time.deltaTime;
            transform.position = (Vector2)followObj.position + MathFG.AngToDir(angle) * distanceToObj;
        }
    }
}