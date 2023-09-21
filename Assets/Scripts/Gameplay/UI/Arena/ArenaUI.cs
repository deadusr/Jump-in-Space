using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.UnityServices;
using JumpInSpace.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Arena {

    [RequireComponent(typeof(UIDocument))]
    public class ArenaUI : MonoBehaviour {

        public Action<Level> selectLevel;
        public Action goBack;

        List<Level> levels;

        VisualElement rootEl;
        VisualElement levelsContainer;
        Button backButton;
        Label username;


        public string Username {
            set => username.text = value;
        }

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();

            rootEl = UIDocument.rootVisualElement;
            levelsContainer = rootEl.Q("LevelsContainer");
            backButton = rootEl.Q<Button>("BackButton");
            username = rootEl.Q<Label>("Username");
        }

        void OnEnable() {
            backButton.clicked += OnClickBackButton;
        }

        void OnDisable() {
            backButton.clicked -= OnClickBackButton;
        }

        public void LoadLevels(List<Level> levels) {
            this.levels = levels;
            RenderLevels();
        }

        void OnClickBackButton() {
            goBack?.Invoke();
        }


        void OnSelectLevel(Level level) {
            selectLevel?.Invoke(level);
        }

        async void RenderLevels() {
            levelsContainer.Clear();


            foreach (var level in levels) {
                var rank = await LeaderboardManager.GetPlayerRank(level.Id);
                var time = await LeaderboardManager.GetPlayerScore(level.Id);
                var scoresCount = await LeaderboardManager.GetScoresCount(level.Id);
                var card = new ArenaLevelCard();
                card.clicked += () => {
                    OnSelectLevel(level);
                };
                levelsContainer.Add(card.Initialize(level.LevelName, $"{rank.ToString()}/{scoresCount}", time.HasValue ? TimeFormat.Format(time.Value) : ""));
            }
        }
    }
}