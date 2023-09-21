using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.UnityServices;
using JumpInSpace.Utils;

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

        List<string> completedLevels = new List<string>();

        public List<Stage> Stages => stages;
        public List<Level> ArenaLevels => arenaLevels;
        public Stage ActiveStage => activeStage;
        public Level ActiveLevel => activeLevel;

        public bool HasNextLevelInStage => activeStage && activeStage.Levels.Count > activeLevelIdx + 1;

        public bool IsActiveLevelAlreadyCompleted => completedLevels.Contains(activeLevel.Id);

        readonly LevelController levelController = new LevelController();

        void Start() {
            RefreshCompletedLevels();
        }
        
        public void LoadStage(Stage stage) {
            activeStage = stage;
            levelController.LoadLevel(stage);
        }

        public void LoadSelectedLevel() {
            levelController.LoadLevel(activeLevel);
            GameplayManager.Instance.ContinueGame();
        }

        public void LoadGarage() {
            SceneManager.LoadScene("Garage");
        }

        public void SelectLevel(Level level) {
            var levels = GameplayManager.Instance.Mode == GameMode.Arena ? arenaLevels : activeStage.Levels;
            int idx = levels.FindIndex(lvl => lvl == level);
            if (idx == -1)
                throw new Exception("Chosen level wasn't been found inside active stage");
            
            activeLevel = level;
            activeLevelIdx = idx;
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

        public async Task CompleteActiveLevel(float completingTime) {
            if(completingTime > ActiveLevel.TimeToComplete)
                return;
            
            var levels = completedLevels;
            levels.Add(activeLevel.Id);
            await CloudSaveManagerService.SavePlayerCompletedLevels(levels);
            RefreshCompletedLevels();
        }

        async void RefreshCompletedLevels() {
            completedLevels = await CloudSaveManagerService.GetPlayerCompletedLevels();
        }

        public bool IsLevelCompleted(Level level) => completedLevels.Contains(level.Id);
    }
}