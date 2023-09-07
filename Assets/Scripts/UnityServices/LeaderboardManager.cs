
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using JumpInSpace.Utils;
using Newtonsoft.Json;

namespace JumpInSpace.UnityServices {
    public class LeaderboardManager : Singleton<LeaderboardManager> {
        const string LeaderboardId = "global_leaderboard";

        string VersionId { get; set; }
        int Offset { get; set; }
        int Limit { get; set; }
        int RangeLimit { get; set; }

        public async void SetScore(float score) {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public async void GetScores() {
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        }

        public async void GetPaginatedScores() {
            Offset = 10;
            Limit = 10;
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        }

        public async Task<float> GetPlayerScore() {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            return (float)scoreResponse.Score;
        }
        
        public async Task<int> GetPlayerRank() {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            return scoreResponse.Rank;
        }

        public async void GetVersionScores() {
            var versionScoresResponse =
                await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
            Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
        }
    }
}