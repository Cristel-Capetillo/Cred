using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using HUD;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Login {
    public class AutoLogin : MonoBehaviour {
        FirebaseAuth auth;

        string deviceID;
        FirebaseUser user;

        async Task Initialize() {
            await FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Result == DependencyStatus.Available) {
                    auth = FirebaseAuth.DefaultInstance;
                    Debug.Log("Logged in!");
                }
                else {
                    Debug.LogError("Could not resolve all Firebase dependencies " + task.Result);
                }
            });
            Debug.Log("Finished Initialization");
        }

        async Task LogIn() {
            Debug.Log("Trying to login");
            user = await Task.Run(() => auth.SignInAnonymouslyAsync());
            Debug.Log($"User ID: {user.UserId}");
        }

        IEnumerator Start() {
            yield return Initialize();
            yield return LogIn();
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("MainScene");
        }
    }
}