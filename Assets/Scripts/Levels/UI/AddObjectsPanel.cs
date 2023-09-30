using System;
using System.Collections.Generic;
using JumpInSpace.Gameplay;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Levels.UI {
    public class AddObjectsPanel : MonoBehaviour {
        public Action<GameplayObject> addGameplayObject;

        VisualElement addObjectsPanel;
        VisualElement gameplayObjectsContainer;

        public List<GameplayObject> GameplayObjects {
            set {
                foreach (var gameplayObject in value) {
                    var card = new GameplayObjectCard();
                    card.clicked += () => {
                        OnAddObject(gameplayObject);
                    };

                    gameplayObjectsContainer.Add(card.Initialize());
                    card.ObjectName = gameplayObject.ObjectName;
                }
            }

        }

        void Awake() {
            var UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement;
            addObjectsPanel = rootEl.Q<VisualElement>("AddObjectsPanel");
            gameplayObjectsContainer = rootEl.Q<VisualElement>("GameplayObjectsContainer");
        }


        public void HidePanel() {
            addObjectsPanel.style.display = DisplayStyle.None;
        }

        public void ShowPanel() {
            addObjectsPanel.style.display = DisplayStyle.Flex;
        }

        void OnAddObject(GameplayObject gameplayObject) {
            addGameplayObject?.Invoke(gameplayObject);
        }
    }
}