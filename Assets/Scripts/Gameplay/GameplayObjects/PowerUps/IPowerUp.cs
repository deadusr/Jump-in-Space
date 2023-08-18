namespace JumpInSpace.Gameplay.GameplayObjects.PowerUps {
    public interface IPowerUp {
        void Apply();
        void End();
        float Duration { get; }
    }
}