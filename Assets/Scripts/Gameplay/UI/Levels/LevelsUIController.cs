using System;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.Levels {
    public class LevelsUIController : MonoBehaviour {
        [SerializeField]
        LevelsUI levelsUI;

        void OnEnable() {
            levelsUI.selectLevel += OnselectLevel;
            levelsUI.goBack += OnGoBack;
        }

        void OnDisable() {
            levelsUI.selectLevel -= OnselectLevel;
            levelsUI.goBack -= OnGoBack;
        }

        void Start() {
            levelsUI.LoadLevels(LevelManager.Instance.ActiveStage.Levels);
        }


        void OnselectLevel(Level level) {
            LevelManager.Instance.LoadLevel(level);
        }
        
        void OnGoBack() {
            GameplayManager.Instance.ShowStages();
        }
    }
}