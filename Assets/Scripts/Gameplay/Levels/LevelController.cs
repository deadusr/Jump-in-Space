namespace JumpInSpace.Gameplay.Levels {
    public class LevelController {
        public void LoadLevel(ILevel level) {
            level.Load();
        }

        public void QuitLevel(ILevel level) {
            level.Quit();
        }

        public void ReplayLevel(ILevel level) {
            level.Quit();
            level.Load();
        }
    }
}