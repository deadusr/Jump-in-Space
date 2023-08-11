using UnityEngine;

namespace Utils {
    public static class MathFG {
        public const float TAU = 6.2831855f;

        public static Vector2 AngToDir(float angInRad) => new Vector2(Mathf.Cos(angInRad), Mathf.Sin(angInRad));

        public static float DirToAng(Vector2 dir) =>
            Mathf.Atan2(dir.y, dir.x);


        public static float WedgeProduct(Vector2 a, Vector2 b) => a.x * b.y - a.y * b.x;
    }
}