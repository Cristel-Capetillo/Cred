using System.Collections;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login {
    public class AutoLogin : MonoBehaviour
    {
        private FirebaseAuth auth;
        
        IEnumerator Start()
        {
            auth = FirebaseAuth.DefaultInstance;
   
            //this seems to be required to NOT create new users for each login
            var dummyUserID = auth.CurrentUser.UserId;

            yield return auth.SignInAnonymouslyAsync().ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }

                var newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                
                Debug.Log("username after sign in: " + auth.CurrentUser.UserId);
           

            }).IsCompleted;
            
            SceneManager.LoadScene("MainScene");
        }
    }
}
