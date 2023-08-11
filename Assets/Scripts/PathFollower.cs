using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utils;

public class PathFollower : MonoBehaviour {
    [SerializeField]
    Path path;

    [SerializeField]
    float speed = 4f;

    bool followPathActive = true;

    Vector2 targetToPositionVec;
    Vector2 toNextNodeVec;

    Vector2 nextNode;

    List<Vector2> nodesWithOffset = new List<Vector2>();



    void Start() {
        var distance = GetDistanceToPoint(transform.position);
        nodesWithOffset = new List<Vector2>();
        foreach (var pathNode in path.Nodes) {
            nodesWithOffset.Add(pathNode);
        }

        // Vector2 vecToAdd = transform.position - path.transform.position;
        //
        // Vector2 prevOffsetDir = Vector2.zero;

        // for (int i = 0; i < path.Nodes.Count; i++) {
        //     bool isFirst = i == 0;
        //     bool isLast = i == path.Nodes.Count - 1;
        //
        //     Vector2 currentNode = path.Nodes[i];
        //     if (isLast) {
        //         nodesWithOffset.Add(currentNode + prevOffsetDir * distance);
        //         break;
        //     }
        //
        //     Vector2 finalOffsetDir = Vector2.zero;
        //     Vector2 offsetDir = Vector2.zero;
        //
        //     // if (isFirst) {
        //     //     Vector2 nextNode = path.Nodes[i + 1];
        //     //     Vector2 toNextVec = nextNode - currentNode;
        //     //     prevOffsetDir = toNextVec.normalized;
        //     // }
        //
        //     if (isLast) {
        //         Vector2 prevNode = path.Nodes[i - 1];
        //         offsetDir = (currentNode - prevNode).normalized;
        //         finalOffsetDir = (offsetDir + prevOffsetDir).normalized;
        //     }
        //     else {
        //         Vector2 nextNode = path.Nodes[i + 1];
        //         Vector2 toNextVec = nextNode - currentNode;
        //         offsetDir = new Vector2(toNextVec.y, -toNextVec.x).normalized;
        //         finalOffsetDir = (offsetDir + prevOffsetDir).normalized;
        //     }
        //
        //
        //     if (isFirst) {
        //         nodesWithOffset.Add(currentNode + finalOffsetDir * distance);
        //     }
        //     else {
        //         float angleBetweenPrevOffset = Mathf.Acos(Vector2.Dot(prevOffsetDir, finalOffsetDir) / prevOffsetDir.magnitude * finalOffsetDir.magnitude);
        //         float length = distance / Mathf.Cos(angleBetweenPrevOffset);
        //         nodesWithOffset.Add(currentNode + finalOffsetDir * length);
        //     }
        //
        //     prevOffsetDir = offsetDir;
        // }

        StartCoroutine(FollowPath());

    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, nextNode, Time.deltaTime * speed);
    }

    void OnDrawGizmos() {
        Handles.color = Color.red;

        if (nodesWithOffset.Count != 0) {
            Vector2 prev = nodesWithOffset[0];
            for (int i = 0; i < nodesWithOffset.Count; i++) {
                // Gizmos.DrawLine(path.Nodes[i], nodesWithOffset[i]);
                Gizmos.DrawLine(prev, nodesWithOffset[i]);
                prev = nodesWithOffset[i];
            }

        }
    }


    float GetDistanceToPoint(Vector2 point) {
        var nodesIdx = GetClosestNodeByDistanceToTransform();

        Vector2 startNode = path.Nodes[nodesIdx.closest];
        Vector2 nextNode = path.Nodes[nodesIdx.next];

        var toNextNodeVec = nextNode - startNode;
        Vector2 toPointVec = point - startNode;

        float startNodeAndPointAngle = Mathf.Acos(Vector2.Dot(toNextNodeVec, toPointVec) / (toNextNodeVec.magnitude * toPointVec.magnitude));
        float distance = toPointVec.magnitude * Mathf.Sin(startNodeAndPointAngle);
        int sign = MathFG.WedgeProduct(toNextNodeVec, toPointVec) < 0 ? 1 : -1;

        return distance * sign;
    }

    IEnumerator FollowPath() {

        var nodesIdx = GetClosestNodeByDistanceToTransform();

        while (followPathActive) {
            nextNode = nodesWithOffset[nodesIdx.next];
            if (nodesIdx.next == 0) {
                transform.position = nodesWithOffset[nodesIdx.next];
            }
            nodesIdx = nodesIdx.next < nodesWithOffset.Count - 1 ? (nodesIdx.next, nodesIdx.next + 1) : (nodesIdx.next, 0);

            yield return new WaitUntil(
                () =>
                    ((Vector2)transform.position - nextNode).magnitude < 0.1f
            );
        }
    }

    (int closest, int next) GetClosestNodeByDistanceToTransform() {
        Vector3 position = transform.position;
        int lastNodeIdx = path.Nodes.Count - 1;

        Vector2 startToPositionVec = position - path.Nodes[0];

        float minDistance = (startToPositionVec).magnitude;
        int idx = 0;

        for (int i = 0; i < path.Nodes.Count; i++) {
            var distance = (position - path.Nodes[i]).magnitude;
            if (minDistance > distance) {
                minDistance = distance;
                idx = i;
            }
        }

        if (idx == lastNodeIdx) {
            return (idx - 1, idx);
        }

        if (idx != 0) {
            var startNode = path.Nodes[idx];
            var nextNode = path.Nodes[idx + 1];
            var toNextNodeVec = nextNode - startNode;
            int sign = (Vector2.Dot(toNextNodeVec, (position - startNode).normalized) / toNextNodeVec.magnitude) < 0 ? -1 : 1;

            return sign == -1 ? (idx - 1, idx) : (idx, idx + 1);
        }

        return (0, 1);
    }
}