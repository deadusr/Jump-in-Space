using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudSave;
using JumpInSpace.Utils;
using Newtonsoft.Json;

namespace JumpInSpace.UnityServices {
    public static class CloudSaveManagerService {
        public static async Task<List<string>> GetPlayerRockets() {
            return await FetchData<List<string>>("player_rockets");
        }

        public static async Task<int> GetPlayerMoney() {
            return await FetchData<int>("player_money");
        }

        public static async Task<List<string>> GetPlayerCompletedLevels() {
            return await FetchData<List<string>>("player_completedLevels");
        }

        public static async Task<List<string>> GetPlayerUnlockedLevels() {
            return await FetchData<List<string>>("player_unlockedLevels");
        }

        public static async Task SavePlayerRockets(List<string> rockets) {
            await SaveData("player_rockets", rockets);
        }

        public static async Task SavePlayerMoney(int money) {
            await SaveData("player_money", money);
        }

        public static async Task SavePlayerCompletedLevels(List<string> levels) {
            await SaveData("player_completedLevels", levels);
        }

        public static async Task SavePlayerUnlockedLevels(List<string> levels) {
            await SaveData("player_unlockedLevels", levels);
        }

        static async Task<T> FetchData<T>(string key) {
            Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });
            return JsonConvert.DeserializeObject<T>(savedData[key]);
        }

        static async Task SaveData<T>(string key, T data) {
            var result = new Dictionary<string, object>() {
                { key, data }
            };
            await CloudSaveService.Instance.Data.ForceSaveAsync(result);
        }

        static async Task DeleteData(string key) {
            await CloudSaveService.Instance.Data.ForceDeleteAsync(key);
        }
    }
}