using UnityEngine;
using UnityEngine.SceneManagement;

namespace JumpInSpace.Gameplay.Levels {
    [CreateAssetMenu(fileName = "Assets/Levels/Level", menuName = "Level", order = 0)]
    public class Level : ScriptableObject, ILevel {

        [SerializeField]
        string levelName;

        [SerializeField]
        string sceneFilename;
        
        public string LevelName => levelName;

        public void Load() {
            SceneManager.LoadScene(sceneFilename);
        }

        public void Quit() {
            Debug.Log($"Quiting scene {levelName}");
        }

    }
}