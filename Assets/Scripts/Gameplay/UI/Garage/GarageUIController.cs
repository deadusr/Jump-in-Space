using System;
using JumpInSpace.Gameplay.Economy;
using JumpInSpace.Gameplay.Levels;
using UnityEngine;

namespace JumpInSpace.Gameplay.UI.Garage {
    public class GarageUIController : MonoBehaviour {
        [SerializeField]
        GarageUI garageUI;

        void OnEnable() {
            EconomyManager.Instance.activeRocketChanged += OnActiveRocketChanged;
            garageUI.prevRocket += OnPrevRocket;
            garageUI.nextRocket += OnNextRocket;
            garageUI.startGame += OnStartGame;
            garageUI.buyRocket += OnBuyRocket;
        }

        void OnDisable() {
            EconomyManager.Instance.activeRocketChanged -= OnActiveRocketChanged;
            garageUI.prevRocket -= OnPrevRocket;
            garageUI.nextRocket -= OnNextRocket;
            garageUI.startGame -= OnStartGame;
            garageUI.buyRocket -= OnBuyRocket;
        }

        void Start() {
            UpdateRocketSpecs(EconomyManager.Instance.ActiveRocket);
        }

        void OnActiveRocketChanged(RocketSpec rocket) {
            UpdateRocketSpecs(rocket);
        }

        void OnPrevRocket() {
            EconomyManager.Instance.PrevRocket();
        }

        void OnNextRocket() {
            EconomyManager.Instance.NextRocket();
        }

        void OnStartGame() {
            LevelManager.Instance.LoadSelectedLevel();
        }

        void OnBuyRocket() {
            EconomyManager.Instance.BuyRocket(
                EconomyManager.Instance.ActiveRocket.Id
            );
        }

        void UpdateRocketSpecs(RocketSpec rocket) {
            garageUI.RocketName = rocket.RocketName;
            garageUI.RocketBoost = rocket.BoostMultiplier.ToString("00");
            garageUI.RocketSpeed = rocket.RocketSpeed.ToString("00");
            garageUI.RocketBoostTime = rocket.BoostTime.ToString("00");
            garageUI.RocketFuelTank = rocket.FuelTank.ToString("00");
            garageUI.RocketOwnedByPlayer = rocket.OwnedByPlayer;
        }
    }
}