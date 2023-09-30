using UnityEngine;

namespace JumpInSpace.Gameplay {
    [CreateAssetMenu(fileName = "GameplayObject", menuName = "Assets/Gameplay/GameplayObject", order = 3)]
    public class GameplayObject : ScriptableObject {
        [SerializeField]
        Transform prefab;

        [SerializeField]
        string id;

        [SerializeField]
        string objectName;

        public Transform Prefab => prefab;
        public string Id => id;

        public string ObjectName => objectName;
    }
}