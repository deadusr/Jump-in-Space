using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using JumpInSpace.UnityServices;

namespace JumpInSpace {
    public class ApplicationController : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }
        async void Start() {
            await AccountManager.Instance.InitUnityServices();
            await AccountManager.Instance.SignInCachedUserAsync();
            SceneManager.LoadScene("MainScreen");
        }
    }
}