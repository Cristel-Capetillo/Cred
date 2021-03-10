using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Firebase;
using Firebase.Auth;


namespace Cred.Scripts
{
    public class DB : MonoBehaviour, IDataBase {
        
        public FirebaseAuth Auth;
        public FirebaseUser User;

        void Awake() {
            Auth = FirebaseAuth.DefaultInstance;
        }

        public IEnumerator Login(string email, string password) {
            var loginTask = Auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.Log($"{email} {password}");

            yield return new WaitUntil( () =>  loginTask.IsCompleted);
            
            
            if (loginTask.Exception != null) {
                FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string message;
                switch (errorCode){
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
                User = loginTask.Result;
                Debug.Log(User.Email);
            }
            //auth.CreateUserWithEmailAndPasswordAsync(username, password);
        }

        public void SignUp(string username, string password) {
            Debug.Log($"{username} signup");
        }
    }
}
