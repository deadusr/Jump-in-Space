using System;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.Arena {
    public class ArenaUIController : MonoBehaviour {
        [SerializeField]
        ArenaUI arenaUI;

        void OnEnable() {
            arenaUI.selectLevel += OnselectLevel;
        }

        void OnDisable() {
            arenaUI.selectLevel -= OnselectLevel;
        }

        void Start() {
            arenaUI.LoadLevels(LevelManager.Instance.ArenaLevels);
        }


        void OnselectLevel(Level level) {
            LevelManager.Instance.LoadLevel(level);
        }
    }
}