using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Firebase;
using Firebase.Auth;

namespace Cred.Scripts
{
    public class DB : MonoBehaviour, IDataBase
    {
        public IEnumerator SignUp(string username, string password) 
        {
            Debug.Log($"{username} and {password}");
        }

        /*public void SignUp(string username, string password) {
            Debug.Log($"{username} and {password} signup");
        }*/
    }
}
