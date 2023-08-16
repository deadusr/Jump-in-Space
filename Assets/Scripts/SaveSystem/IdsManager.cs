using System;
using System.Collections.Generic;
using UnityEngine;

namespace JumpInSpace.SaveSystem {

    [DefaultExecutionOrder(-1)]
    [ExecuteInEditMode]
    public class IdsManager : MonoBehaviour {
        Dictionary<string, int> idToInstanceId = new Dictionary<string, int>();
        Dictionary<string, GameObject> idToGameObject = new Dictionary<string, GameObject>();

        public static IdsManager Instance { get; private set; }

        public Dictionary<string, int> IdToInstanceId => idToInstanceId;
        public Dictionary<string, GameObject> IdToGameObject => idToGameObject;

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            }
            else {
                Instance = this;
            }


        }

        void Update() {
            if (Instance == null) {
                Instance = FindObjectOfType<IdsManager>();
            }
        }

        public void Add(GenID genID) {
            if (
                idToInstanceId.ContainsKey(genID.Id)
            ) {
                return;
            }
            idToInstanceId.Add(genID.Id, genID.GetInstanceID());
            idToGameObject.Add(genID.Id, genID.gameObject);
        }

        public void Remove(string id) {
            idToInstanceId.Remove(id);
            idToGameObject.Remove(id);
        }
        
        public bool Validate(GenID genID) {
            if (idToInstanceId.TryGetValue(genID.Id, out var instanceId)) {
                return genID.GetInstanceID() == instanceId;
            }
            return true;
        }
    }
}