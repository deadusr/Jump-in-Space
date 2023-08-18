using System.Collections.Generic;
using JumpInSpace.Utils;
using UnityEngine;

namespace JumpInSpace.Gameplay.Levels {
    public class LevelManager : Singleton<LevelManager> {
        [SerializeField]
        List<Level> levels;

        public List<Level> Levels => levels;
        public int LevelsCount => levels.Count;
        public Level ActiveLevel => activeLevel;

        readonly LevelController levelController = new LevelController();
        
        Level activeLevel;

        public void LoadLevel(Level level) {
            levelController.LoadLevel(level);
            activeLevel = level;
        }

        public void ReplayLevel() {
            if(activeLevel == null) 
                return;
            levelController.ReplayLevel(activeLevel);
        }

        public void QuitLevel() {
            if(activeLevel == null)
                return;
            levelController.QuitLevel(activeLevel);
            GameplayManager.Instance.ShowLevels();
        }
    }
}