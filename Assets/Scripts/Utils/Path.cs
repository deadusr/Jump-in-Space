using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JumpInSpace.Utils {
    public class Path : MonoBehaviour {

        List<Vector3> nodes = new List<Vector3>();

        void Awake() {
            var children = GetComponentsInChildren<Node>();
            foreach (var node in children) {
                nodes.Add(node.transform.position);
            }
        }
        void OnDrawGizmos() {
            var nodes = GetComponentsInChildren<Node>();
            for (int i = 0; i < nodes.Length - 1; i++) {
                Gizmos.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position);
            }
        }


        public List<Vector3> Nodes => nodes;


        public void FollowPath(Transform obj, float speed) {
            StartCoroutine(FollowPathCoroutine(obj, speed));
        }

        IEnumerator FollowPathCoroutine(Transform obj, float speed) {
            int currentTargetIdx = nodes.Count - 1;
            // int direction = +1;
            while (true) {
                ;
                var currentTarget = nodes[currentTargetIdx];
                obj.position = Vector3.MoveTowards(obj.position, currentTarget, Time.deltaTime * speed);
                if (obj.position == currentTarget) {
                    if (currentTargetIdx >= nodes.Count - 1) {
                        currentTargetIdx = 0;
                        obj.position = nodes[currentTargetIdx]; // reset position to zero
                    }
                    // if (currentTargetIdx == 0) {
                    //     currentTargetIdx = nodes.Count - 1;
                    //     obj.position = nodes[currentTargetIdx];
                    // }
                    else {
                        currentTargetIdx += 1;
                    }
                }
                yield return null;
            }
        }
    }
}