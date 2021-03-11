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
        public InputField UsernameText;
        public InputField PasswordText;
        public GameObject SubmitButton;
        // Start is called before the first frame update
        void Start() {
            SubmitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
        }

        public void LoginClick() 
        {
            var username = UsernameText.text;
            var password = PasswordText.text;
            if (username == "" || password == "")
            {
                Debug.Log("Please enter both username and password");
                return;
            }
            StartCoroutine(GetComponent<DB>().Login(username,password));
        }

        public void SignUpClick() 
        {
            var username = UsernameText.text;
            var password = PasswordText.text;
            if (username == "" || password == "")
            {
                Debug.Log("Please enter both username and password");
                return;
            }
            StartCoroutine(GetComponent<DB>().SignUp(username,password));
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
