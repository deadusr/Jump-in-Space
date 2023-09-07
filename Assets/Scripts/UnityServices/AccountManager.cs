using System.Threading.Tasks;
using JumpInSpace.Utils;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

namespace JumpInSpace.UnityServices {
    public class AccountManager : Singleton<AccountManager> {
        public bool IsSignIn => AuthenticationService.Instance.IsAuthorized;
        public string Username => AuthenticationService.Instance.PlayerInfo.Username;

        public async Task InitUnityServices() => await Unity.Services.Core.UnityServices.InitializeAsync();

        public async Task SignUpWithUsernamePasswordAsync(string username, string password) =>
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);

        public async Task SignInWithUsernamePasswordAsync(string username, string password) =>
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);


        public async Task AddUsernamePasswordAsync(string username, string password) {
            try {
                await AuthenticationService.Instance.AddUsernamePasswordAsync(username, password);
                Debug.Log("Username and password added.");
            }
            catch (AuthenticationException ex) {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex) {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

        public async Task SignInAnonymouslyAsync() {
            try {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");

            }
            catch (AuthenticationException ex) {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex) {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

        public async Task SignInCachedUserAsync() {
            // Check if a cached player already exists by checking if the session token exists
            if (!AuthenticationService.Instance.SessionTokenExists) {
                // if not, then do nothing
                return;
            }

            // Sign in Anonymously
            // This call will sign in the cached player.
            try {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");

                // Shows how to get the playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            }
            catch (AuthenticationException ex) {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex) {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }

    }
}