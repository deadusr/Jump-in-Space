using System;
using System.Collections.Generic;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.Levels {
    public class LevelManager : Singleton<LevelManager> {
        [SerializeField]
        List<Stage> stages;

        [SerializeField]
        List<Level> arenaLevels;

        [SerializeField]
        Level activeLevel;
        int activeLevelIdx;

        [SerializeField]
        Stage activeStage;

        public List<Stage> Stages => stages;
        public List<Level> ArenaLevels => arenaLevels;
        public Stage ActiveStage => activeStage;
        public Level ActiveLevel => activeLevel;

        public bool HasNextLevelInStage => activeStage && activeStage.Levels.Count > activeLevelIdx + 1;

        readonly LevelController levelController = new LevelController();


        public void LoadStage(Stage stage) {
            activeStage = stage;
            levelController.LoadLevel(stage);
        }

        public void LoadLevel(Level level) {
            var levels = GameplayManager.Instance.Mode == GameMode.Arena ? arenaLevels : activeStage.Levels;
            int idx = levels.FindIndex(lvl => lvl == level);
            if (idx == -1)
                throw new Exception("Chosen level wasn't been found inside active stage");

            levelController.LoadLevel(level);
            activeLevel = level;
            activeLevelIdx = idx;
            GameplayManager.Instance.ContinueGame();
        }

        public void LoadNextLevelInStage() {
            int nextIdx = activeLevelIdx + 1;
            if (activeStage.Levels.Count > nextIdx) {
                levelController.LoadLevel(activeStage.Levels[nextIdx]);
                activeLevelIdx = nextIdx;
                activeLevel = activeStage.Levels[nextIdx];
            }
        }

        public void ReplayLevel() {
            if (activeLevel == null)
                return;
            levelController.ReplayLevel(activeLevel);
        }
    }
}