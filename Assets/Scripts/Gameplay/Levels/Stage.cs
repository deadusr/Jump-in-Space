using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.Gameplay.Levels;

namespace JumpInSpace.Gameplay.Levels {
    [CreateAssetMenu(fileName = "Assets/Levels/Stage", menuName = "Stage", order = 0)]
    public class Stage : ScriptableObject, ILoadable {

        [SerializeField]
        string stageName;

        [SerializeField]
        List<Level> levels;

        public string StageName => stageName;

        public List<Level> Levels => levels;

        public void Load() {
            SceneManager.LoadScene("Levels");
        }

        public void Quit() {
            Debug.Log($"Quiting stage {stageName}");
        }
    }
}