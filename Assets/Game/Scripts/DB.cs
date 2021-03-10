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

        public IEnumerator SignUp(string email, string password) {
            Debug.Log($"{email} signup");
            
            var RegisterTask = Auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
        }
    }
}
