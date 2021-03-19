using System.Collections;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login {
    public class AutoLogin : MonoBehaviour
    {
        private FirebaseAuth auth;
        Task tmp;

        string deviceID;
        async void Start() {
            deviceID = SystemInfo.deviceUniqueIdentifier;
            print($"Device ID: {deviceID}");
            auth = FirebaseAuth.DefaultInstance;
            //this seems to be required to NOT create new users for each login
            //var dummy = auth.CurrentUser.UserId;
            await auth.SignInAnonymouslyAsync();

            await auth.SignInAnonymouslyAsync().ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }
                
                print($"result is: {task.Result.UserId}");

                var newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                
                
                Debug.Log("username after sign in: " + auth.CurrentUser.UserId);
           

            });
            SceneManager.LoadScene("MainScene");
        }
    }
}
