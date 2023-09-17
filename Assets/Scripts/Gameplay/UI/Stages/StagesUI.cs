using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Stages {

    [RequireComponent(typeof(UIDocument))]
    public class StagesUI : MonoBehaviour {

        public Action<Stage> selectStage;
        public Action goBack;

        List<Stage> stages;

        VisualElement rootEl;
        VisualElement container;
        Button backButton;



        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            rootEl = UIDocument.rootVisualElement;
            container = rootEl.Q("Container");
            backButton = rootEl.Q<Button>("BackButton");
        }

        void OnEnable() {
            backButton.clicked += OnClickBackButton;
        }

        void OnDisable() {
            backButton.clicked -= OnClickBackButton;
        }
        public void LoadStages(List<Stage> stages) {
            this.stages = stages;
            RenderStages();
        }


        void OnSelectStage(Stage stage) {
            selectStage?.Invoke(stage);
        }

        void OnClickBackButton() {
            goBack?.Invoke();
        }

        void RenderStages() {
            container.Clear();
            foreach (var stage in stages) {
                var card = new StageCard();
                card.clicked += () => {
                    OnSelectStage(stage);
                };
                container.Add(card.Initialize(stage.StageName));
            }
        }
    }
}