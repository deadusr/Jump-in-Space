using System;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.Player {
    public class PlayerManager : Singleton<PlayerManager> {
        public Action winLevel;
        public Action<string> loseLevel;


        public void WinLevel() {
            winLevel?.Invoke();
        }

        public void LoseLevel(string reason) {
            loseLevel?.Invoke(reason);
        }
    }
}