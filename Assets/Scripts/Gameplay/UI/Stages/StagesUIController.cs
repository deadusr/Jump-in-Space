using System;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.Stages {
    public class StagesUIController : MonoBehaviour {
        [SerializeField]
        StagesUI stagesUI;

        void OnEnable() {
            stagesUI.selectStage += OnselectStage;
            stagesUI.goBack += OnGoBack;
        }

        void OnDisable() {
            stagesUI.selectStage -= OnselectStage;
            stagesUI.goBack -= OnGoBack;
        }

        void Start() {
            stagesUI.LoadStages(LevelManager.Instance.Stages);
        }


        void OnselectStage(Stage stage) {
            LevelManager.Instance.LoadStage(stage);
        }
        
        void OnGoBack() {
            GameplayManager.Instance.ShowHomePage();
        }
    }
}