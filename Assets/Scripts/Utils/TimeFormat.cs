using UnityEngine;

namespace JumpInSpace.Utils {
    public static class TimeFormat {
        public static string Format(float t) {
            float min = Mathf.Floor(t / 60);
            float sec = Mathf.Floor(t - (min * 60));
            float mSec = Mathf.Floor((t - sec - min * 60) * 100);
            string minStr = min != 0 ? $"{min:00}min " : "";
            return $"{minStr}{sec}.{mSec:000} s";
        }
    }
}