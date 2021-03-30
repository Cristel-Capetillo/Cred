using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login {
    public class AutoLogin : MonoBehaviour {
        FirebaseAuth auth;
        FirebaseUser user;

        async Task Initialize() {
            await FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Result == DependencyStatus.Available) {
                    auth = FirebaseAuth.DefaultInstance;
                    Debug.Log("[AutoLogin_Initialize]\nLogged in!");
                } else {
                    Debug.LogError("[AutoLogin_Initialize]\nCould not resolve all Firebase dependencies " + task.Result);
                }
            });
            Debug.Log("[AutoLogin_Initialize]\nFinished Initialization");
        }

        async Task LogIn() {
            Debug.Log("[AutoLogin_LogIn]\nTrying to login");
            user = await Task.Run(() => auth.SignInAnonymouslyAsync());
            Debug.Log($"[AutoLogin_LogIn]\nUser ID: {user.UserId}");
        }

        IEnumerator Start() {
            var initTask = Initialize();
            while (!initTask.IsCompleted) {
                yield return null;
            }

            var loginTask = LogIn();
            while (!loginTask.IsCompleted) {
                yield return null;
            }

            SceneManager.LoadScene("MainScene");
        }
    }
}