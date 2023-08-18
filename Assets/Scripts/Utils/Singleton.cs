using System;
using UnityEngine;

namespace JumpInSpace.Utils {
    public class Singleton<T> : MonoBehaviour where T : Component {
        static T instance;

        public static T Instance {
            get {
                if (instance == null) {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null) {
                        SetupInstance();
                    }
                    else {
                        string typeName = typeof(T).Name;

                        Debug.Log("[Singleton] " + typeName + " instance already created: " +
                            instance.gameObject.name);
                    }
                }
                return instance;
            }
        }

        void Awake() {
            if (instance == null) {
                instance = this as T;
                if (Application.isPlaying)
                    DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }


        static void SetupInstance() {
            GameObject gameObject =
                new GameObject {
                    name = typeof(T).Name
                };
            instance = gameObject.AddComponent<T>();
            if (Application.isPlaying)
                DontDestroyOnLoad(gameObject);
        }
    }
}