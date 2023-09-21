using UnityEngine;
using UnityEngine.SceneManagement;

namespace JumpInSpace.Gameplay.Levels {
    [CreateAssetMenu(fileName = "Assets/Levels/Level", menuName = "Level", order = 0)]
    public class Level : ScriptableObject, ILoadable {

        [SerializeField]
        string levelName;

        [SerializeField]
        string sceneFilename;
        
        [SerializeField]
        string id;

        [Header("Economy")]
        [SerializeField]
        int priceForCompletingLevel;
        
        [SerializeField]
        float timeToCompleteLevelSec;

        public string LevelName => levelName;
        public string Id => id;

        public int MoneyForCompleting => priceForCompletingLevel;
        public float TimeToComplete => timeToCompleteLevelSec;

        public void Load() {
            SceneManager.LoadScene(sceneFilename);
        }

        public void Quit() {
            Debug.Log($"Quiting scene {levelName}");
        }

    }
}