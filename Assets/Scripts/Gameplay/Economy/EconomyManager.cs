using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JumpInSpace.Gameplay.Levels;
using JumpInSpace.Gameplay.Player;
using JumpInSpace.UnityServices;
using UnityEngine;
using JumpInSpace.Utils;

namespace JumpInSpace.Gameplay.Economy {
    public class EconomyManager : Singleton<EconomyManager> {

        public Action<int> moneyAmountChanged;
        public Action<RocketSpec> activeRocketChanged;

        [SerializeField]
        GameplayData gameplayData;

        int playerMoney;
        Dictionary<string, RocketSpec> playerRockets = new Dictionary<string, RocketSpec>();

        RocketSpec activeRocket;

        public RocketSpec ActiveRocket => activeRocket;
        bool CanPlayerAffordRocket(string rocketId) => gameplayData.Rockets[rocketId].Price <= playerMoney;
        bool DoesPlayerHaveThisRocket(string rocketId) => gameplayData.Rockets[rocketId].OwnedByPlayer;
        public int PlayerMoney => playerMoney;
        void Start() {
            RefreshMoney();
            RefreshRockets();
        }

        void OnEnable() {
            PlayerManager.Instance.winLevel += OnPlayerCompleteLevel;
        }

        void OnDisable() {
            PlayerManager.Instance.winLevel -= OnPlayerCompleteLevel;
        }

        public async void BuyRocket(string rocketId) {
            if (!gameplayData.Rockets.ContainsKey(rocketId))
                return;
            if (!CanPlayerAffordRocket(rocketId))
                return;
            if (DoesPlayerHaveThisRocket(rocketId))
                return;

            int moneyLeft = playerMoney - gameplayData.Rockets[rocketId].Price;

            var rocketsIds = playerRockets.Keys.ToList();
            rocketsIds.Add(rocketId);

            await UpdateMoney(moneyLeft);
            await UpdateRockets(rocketsIds);
        }

        public void PrevRocket() {
            var keys = gameplayData.Rockets.Keys.ToList();
            var idx = keys.FindIndex(id => id == activeRocket.Id);

            if (idx > 0) {
                idx -= 1;
            }
            else {
                idx = keys.Count - 1;
            }


            activeRocket = gameplayData.Rockets[keys[idx]];
            OnActiveRocketChanged();
        }

        public void NextRocket() {
            var keys = gameplayData.Rockets.Keys.ToList();
            var idx = keys.FindIndex(id => id == activeRocket.Id);

            if (idx >= keys.Count - 1) {
                idx = 0;
            }
            else {
                idx += 1;
            }


            activeRocket = gameplayData.Rockets[keys[idx]];
            OnActiveRocketChanged();
        }

        void OnActiveRocketChanged() {
            activeRocketChanged?.Invoke(activeRocket);
        }

        void OnPlayerCompleteLevel(float completionTime) {
            if (completionTime > LevelManager.Instance.ActiveLevel.TimeToComplete)
                return;
            if (LevelManager.Instance.IsActiveLevelAlreadyCompleted)
                return;

            AddMoney(LevelManager.Instance.ActiveLevel.MoneyForCompleting);
        }

        async void RefreshRockets() {
            var rocketIds = await CloudSaveManagerService.GetPlayerRockets();

            playerRockets = new Dictionary<string, RocketSpec>();

            foreach (var rocketId in rocketIds) {
                if (gameplayData.Rockets[rocketId] != null)
                    playerRockets.Add(rocketId, gameplayData.Rockets[rocketId]);
                else {
                    Debug.LogError($"Rocket with id {rocketId} wasn't found in rockets list");
                }
            }

            foreach (var rocketsValue in gameplayData.Rockets.Values) {
                if (!rocketsValue.OwnedByPlayer)
                    rocketsValue.OwnedByPlayer = rocketIds.Contains(rocketsValue.Id);
            }

            activeRocket = gameplayData.Rockets.Values.First();
            OnActiveRocketChanged();
        }

        async void RefreshMoney() {
            playerMoney = await CloudSaveManagerService.GetPlayerMoney();

            moneyAmountChanged?.Invoke(playerMoney);
        }

        async Task UpdateMoney(int money) {
            await CloudSaveManagerService.SavePlayerMoney(money);
            RefreshMoney();
        }

        async Task UpdateRockets(List<string> rockets) {
            await CloudSaveManagerService.SavePlayerRockets(rockets);
            RefreshRockets();
        }

        async void AddMoney(int moneyAmount) {
            Debug.Log($"Adding money {moneyAmount}");
            int newValue = playerMoney + moneyAmount;
            await UpdateMoney(newValue);
        }

    }
}