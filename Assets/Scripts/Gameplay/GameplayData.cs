using System;
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
        
        [SerializeField] List<GameplayObject> gameplayObjects;
        // Dictionary<string, GameplayObject> gameplayObjectsDict = new Dictionary<string, GameplayObject>();

        public List<GameplayObject> GameplayObjects => gameplayObjects;

        // void Awake() {
        //     foreach (var gameplayObject in gameplayObjects) {
        //         gameplayObjectsDict.Add(gameplayObject.Id, gameplayObject);
        //     }
        // }
        //
        public GameplayObject GetGameplayObject(string id) => gameplayObjects.Find(el => el.Id == id);

        public Dictionary<string, RocketSpec> Rockets => rockets.ToDictionary(k => k.Id);
    }
}