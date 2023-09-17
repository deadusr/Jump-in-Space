// using System;
// using System.Collections.Generic;
// using JumpInSpace.Editor;
// using UnityEngine;
//
// namespace JumpInSpace.SaveSystem {
//     public class SaveSystemController : MonoBehaviour {
//
//         [SerializeField]
//         string filepath;
//
//         public static SaveSystemController Instance { get; private set; }
//
//         void Awake() {
//             if (Instance != null && Instance != this) {
//                 Destroy(this);
//             }
//             else {
//                 Instance = this;
//             }
//
//             DontDestroyOnLoad(gameObject);
//         }
//
//         public void Save() {
//             GameSave save = new GameSave();
//             save.Positions = new Dictionary<string, SerializableVector3>();
//             save.Rotations = new Dictionary<string, SerializableQuaternion>();
//             foreach (var (id, go) in IdsManager.Instance.IdToGameObject) {
//                 save.Positions.Add(id, go.transform.position);
//                 save.Rotations.Add(id, go.transform.rotation);
//             }
//             Write(save);
//             Debug.Log("Saved game");
//         }
//
//
//         public GameSave? LoadSave() {
//             var save = Read();
//             return save;
//         }
//
//         void Write(GameSave save) {
//             System.IO.Stream s = new System.IO.FileStream($"{Application.dataPath}/{filepath}", System.IO.FileMode.Create, System.IO.FileAccess.Write);
//             System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
//             formatter.Serialize(s, save);
//             s.Close();
//         }
//         GameSave? Read() {
//
//             try {
//                 System.IO.Stream s = new System.IO.FileStream($"{Application.dataPath}/{filepath}", System.IO.FileMode.Open, System.IO.FileAccess.Read);
//                 System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
//                 GameSave save = (GameSave)formatter.Deserialize(s);
//                 s.Close();
//                 return save;
//             }
//             catch (Exception err) {
//                 Debug.Log(err);
//             }
//             return null;
//         }
//     }
//
//
//     [Serializable]
//     public struct GameSave {
//         public Dictionary<string, SerializableVector3> Positions;
//         public Dictionary<string, SerializableQuaternion> Rotations;
//     }
// }