using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.UnityServices;

namespace JumpInSpace {
    public class ApplicationController : MonoBehaviour {

        [SerializeField]
        GameObject setupGameObject;
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }
        async void Start() {
            await AccountManagerService.InitUnityServices();
            await AccountManagerService.SignInCachedUserAsync();
            setupGameObject.SetActive(true);
            SceneManager.LoadScene("MainScreen");
        }
    }
}