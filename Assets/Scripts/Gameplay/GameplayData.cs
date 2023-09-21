using System.Collections.Generic;
using System.Linq;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay {
    [CreateAssetMenu(fileName = "Assets/GameplayData", menuName = "GameplayData", order = 2)]
    public class GameplayData: ScriptableObject {
        [SerializeField]
        List<RocketSpec> rockets;

        [SerializeField]
        List<Stage> stages;

        public Dictionary<string, RocketSpec> Rockets => rockets.ToDictionary(k => k.Id);
    }
}