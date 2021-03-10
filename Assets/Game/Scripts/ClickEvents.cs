using System;
using System.Collections;
using System.Collections.Generic;
using Cred.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Cred
{
    public class ClickEvents : MonoBehaviour
    {
        public GameObject UsernameText;
        public GameObject PasswordText;
        public GameObject SubmitButton;
        // Start is called before the first frame update
        void Awake() {
            SubmitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
        }

        public void LoginClick() 
        {
            var email = UsernameText.GetComponent<InputField>().text;
            var password = PasswordText.GetComponent<InputField>().text;
            if (email == "" || password == "")
            {
                Debug.Log("Please enter both username and password");
                return;
            }
            StartCoroutine(GetComponent<DB>().Login(email,password));
        }

        public void SignUpClick() 
        {
            var username = UsernameText.GetComponent<Text>().text;
            var password = PasswordText.GetComponent<Text>().text;
            if (username == "" || password == "")
            {
                Debug.Log("Please enter both username and password");
                return;
            }
            GetComponent<DB>().SignUp(username,password);
        }

        public void LoginToggleButton() {
            SubmitButton.transform.Find("Text").GetComponent<Text>().text = "Login";
            SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            SubmitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
        }
        public void SignUpToggleButton() {
            SubmitButton.transform.Find("Text").GetComponent<Text>().text = "Sign Up";
            SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            SubmitButton.GetComponent<Button>().onClick.AddListener(SignUpClick);
        }
        
    }
}
