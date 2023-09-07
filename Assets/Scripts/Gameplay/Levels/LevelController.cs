namespace JumpInSpace.Gameplay.Levels {
    public class LevelController {
        public void LoadLevel(ILoadable level) {
            level.Load();
        }

        public void QuitLevel(ILoadable level) {
            level.Quit();
        }

        public void ReplayLevel(ILoadable level) {
            level.Quit();
            level.Load();
        }
    }
}