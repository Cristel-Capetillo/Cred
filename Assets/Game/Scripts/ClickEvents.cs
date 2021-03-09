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
        public void LoginClick() 
        {
            var username = UsernameText.GetComponent<Text>().text;
            var password = PasswordText.GetComponent<Text>().text;
            if (username == "" || password == "")
            {
                Debug.Log("Please enter both username and password");
                return;
            }
            GetComponent<DB>().Login(username,password);
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
            SubmitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
            
        }
        public void SignUpToggleButton() {
            SubmitButton.transform.Find("Text").GetComponent<Text>().text = "SignUp";
            SubmitButton.GetComponent<Button>().onClick.AddListener(SignUpClick);
        }
        
    }
}
