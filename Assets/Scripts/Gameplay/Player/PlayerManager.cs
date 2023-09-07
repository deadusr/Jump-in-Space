using System;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.Player {
    public class PlayerManager : Singleton<PlayerManager> {
        public Action<float> winLevel;
        public Action<string> loseLevel;


        public void WinLevel(float levelTime) {
            winLevel?.Invoke(levelTime);
        }

        public void LoseLevel(string reason) {
            loseLevel?.Invoke(reason);
        }
    }
}