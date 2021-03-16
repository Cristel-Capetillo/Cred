using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLogin : MonoBehaviour
{
    public FirebaseAuth auth;
    
    // UserData.UserID = User.UserId;
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        FirebaseUser currentUser = auth.CurrentUser;
        if (currentUser != null) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
        //Debug.Log(SystemInfo.deviceUniqueIdentifier);

        auth.SignInWithCustomTokenAsync(SystemInfo.deviceUniqueIdentifier).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithCustomTokenAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithCustomTokenAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        
        //SceneManager.LoadScene("MainScene");
        
    }

    
    void Update()
    {
        
    }
}
