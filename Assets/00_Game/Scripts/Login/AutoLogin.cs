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
            while (!Initialize().IsCompleted) {
                yield return null;
            }

            while (!LogIn().IsCompleted) {
                yield return null;
            }

            SceneManager.LoadScene("MainScene");
        }
    }
}