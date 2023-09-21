using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JumpInSpace.Gameplay.UI.Garage {

    [RequireComponent(typeof(UIDocument))]
    public class GarageUI : MonoBehaviour {
        public Action startGame;
        public Action buyRocket;
        public Action prevRocket;
        public Action nextRocket;

        Label rocketName;
        Label rocketSpeed;
        Label rocketBoost;
        Label rocketBoostTime;
        Label rocketFuelTank;
        Button startGameOrBuyButton;
        Button prevButton;
        Button nextButton;

        bool activeRocketOwnedByPlayer;

        public string RocketName {
            set => rocketName.text = value;
        }
        public string RocketSpeed {
            set => rocketSpeed.text = value;
        }

        public string RocketBoost {
            set => rocketBoost.text = value;
        }

        public string RocketBoostTime {
            set => rocketBoostTime.text = value;
        }

        public string RocketFuelTank {
            set => rocketFuelTank.text = value;
        }

        public bool RocketOwnedByPlayer {
            set {
                if (value) {
                    startGameOrBuyButton.text = "Start game";
                }
                else {
                    startGameOrBuyButton.text = "Buy rocket";
                }
                activeRocketOwnedByPlayer = value;
            }

        }

        void Awake() {
            UIDocument UIDocument = GetComponent<UIDocument>();
            var rootEl = UIDocument.rootVisualElement;

            rocketName = rootEl.Q<Label>("RocketName");
            rocketSpeed = rootEl.Q<Label>("RocketSpeed");
            rocketBoost = rootEl.Q<Label>("RocketBoost");
            rocketBoostTime = rootEl.Q<Label>("RocketBoostTime");
            rocketFuelTank = rootEl.Q<Label>("RocketFuelTank");
            startGameOrBuyButton = rootEl.Q<Button>("StartGameButton");
            prevButton = rootEl.Q<Button>("PrevButton");
            nextButton = rootEl.Q<Button>("NextButton");
        }

        void OnEnable() {
            startGameOrBuyButton.clicked += OnStartGameOrBuy;
            prevButton.clicked += OnPrevRocket;
            nextButton.clicked += OnNextRocket;
        }

        void OnDisable() {
            startGameOrBuyButton.clicked -= OnStartGameOrBuy;
            prevButton.clicked -= OnPrevRocket;
            nextButton.clicked -= OnNextRocket;
        }

        void OnStartGameOrBuy() {
            if (activeRocketOwnedByPlayer)
                startGame?.Invoke();
            else {
                buyRocket?.Invoke();
            }
        }

        void OnPrevRocket() {
            prevRocket?.Invoke();
        }

        void OnNextRocket() {
            nextRocket?.Invoke();
        }
    }
}