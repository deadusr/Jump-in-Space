// using System;
// using UnityEditor;
// using UnityEngine;
// using JumpInSpace.Editor;
//
// namespace JumpInSpace.SaveSystem {
//
//     [ExecuteInEditMode]
//     public class GenID : MonoBehaviour {
//         [ReadOnly] public string Id;
//
//         void Awake() {
//             AddIdToManager();
//         }
//
//         void AddIdToManager() {
//             if (Id == null) {
//                 Id = Guid.NewGuid().ToString();
//             }
//             else {
//                 bool valid = IdsManager.Instance.Validate(this);
//                 if (!valid) {
//                     Id = Guid.NewGuid().ToString();
//                 }
//             }
//
//             IdsManager.Instance.Add(this);
//         }
//
//         void OnDestroy() {
//             if (Time.time != 0) { // Fixing issue when OnDestroy was called when entering play mode
//                 IdsManager.Instance.Remove(Id);
//             }
//         }
//     }
// }