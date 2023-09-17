using UnityEngine;

namespace JumpInSpace.Utils {
    public static class DrawingExtension {
        public static void DrawCircle(this GameObject container, float radius, float lineWith, Color color) {
            int segments = 360;
            int pointsCount = segments + 1;
            var line = container.GetComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.startWidth = lineWith;
            line.endWidth = lineWith;
            line.positionCount = pointsCount;

            var points = new Vector3[pointsCount];
            for (int i = 0; i < points.Length; i++) {
                float degreeRad = (i / (pointsCount - 1f)) * MathFG.TAU;
                var result = MathFG.AngToDir(degreeRad);
                points[i] = new Vector3(result.x * radius, result.y * radius, 0);
            }

            line.SetPositions(points);
        }
    }
}