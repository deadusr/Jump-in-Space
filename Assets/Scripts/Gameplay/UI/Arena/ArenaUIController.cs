using System;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.UnityServices;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.Arena {
    public class ArenaUIController : MonoBehaviour {
        [SerializeField]
        ArenaUI arenaUI;

        void OnEnable() {
            arenaUI.selectLevel += OnselectLevel;
            arenaUI.goBack += OnGoBack;
        }

        void OnDisable() {
            arenaUI.selectLevel -= OnselectLevel;
        }

        void Start() {
            arenaUI.LoadLevels(LevelManager.Instance.ArenaLevels);
            arenaUI.Username = AccountManager.Instance.Username;
        }


        void OnselectLevel(Level level) {
            LevelManager.Instance.LoadLevel(level);
        }


        void OnGoBack() {
            GameplayManager.Instance.ShowHomePage();
        }
    }
}