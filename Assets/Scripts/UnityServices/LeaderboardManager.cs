using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using JumpInSpace.Utils;
using Newtonsoft.Json;

namespace JumpInSpace.UnityServices {
    public class LeaderboardManager : Singleton<LeaderboardManager> {

        string VersionId { get; set; }
        int Offset { get; set; }
        int Limit { get; set; }
        int RangeLimit { get; set; }

        public async void SetScore(string leaderboardId, float score) {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public async Task<int> GetScoresCount(string leaderboardId) {
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
            return scoresResponse.Total;
        }

        public async void GetPaginatedScores(string leaderboardId) {
            Offset = 10;
            Limit = 10;
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        }

        public async Task<float?> GetPlayerScore(string leaderboardId) {
            try {
                var scoreResponse =
                    await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
                return (float)scoreResponse.Score;
            }
            catch {
                return null;
            }
        }

        public async Task<int?> GetPlayerRank(string leaderboardId) {
            try {
                var scoreResponse =
                    await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
                return scoreResponse.Rank + 1; // rank should starts from 1, not zero
            }
            catch {
                return null;
            }
        }

        public async void GetVersionScores(string leaderboardId) {
            var versionScoresResponse =
                await LeaderboardsService.Instance.GetVersionScoresAsync(leaderboardId, VersionId);
            Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
        }
    }
}