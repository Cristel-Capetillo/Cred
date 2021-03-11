using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

namespace Cred.Scripts {
    public class DB : MonoBehaviour, IDataBase {
        public FirebaseAuth auth;
        public FirebaseUser User;
        public string UserId;

        void Start() {
            auth = FirebaseAuth.DefaultInstance;
        }

        public IEnumerator Login(string email, string password) {
            //Debug.Log($"{email} and {password}");
            var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null) {
                //If there are errors handle them
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode) {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        Debug.Log(message);
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        Debug.Log(message);
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        Debug.Log(message);
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        Debug.Log(message);
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        Debug.Log(message);
                        break;
                }

                

            }
            else {
                //User is now logged in
                //Now get the result
                User = LoginTask.Result;
                Debug.Log(User.Email);
                UserData.UserID = User.UserId;
                SceneManager.LoadScene("MainScene");

            }
        }

    }
}



/*public void SignUp(string username, string password) {
    Debug.Log($"{username} and {password} signup");
}*/
 
