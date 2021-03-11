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
        public GameObject LoginToggle;
        public GameObject SignUpToggle;
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
            //change submit button style
            SubmitButton.transform.Find("Text").GetComponent<Text>().text = "Login";
            SubmitButton.GetComponent<Image>().color =  new Color(1f,0.19215686274f,0.6862745098f, 1);
            
            //change submitbutton functionality
            SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            SubmitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
            
            //change toggle buttons style
            LoginToggle.GetComponent<RectTransform>().localScale = new Vector3(0.2f,0.39f, 0.5982905f);
            SignUpToggle.GetComponent<RectTransform>().localScale = new Vector3(0.2f,0.3f, 0.5982905f);

        }
        public void SignUpToggleButton() {
            SubmitButton.transform.Find("Text").GetComponent<Text>().text = "Sign Up";
            SubmitButton.GetComponent<Image>().color =  new Color(0.16078431372f,0.72549019607f,0.98039215686f, 1);
            
            SubmitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            SubmitButton.GetComponent<Button>().onClick.AddListener(SignUpClick);
            
            LoginToggle.GetComponent<RectTransform>().localScale = new Vector3(0.2f,0.3f, 0.5982905f);
            SignUpToggle.GetComponent<RectTransform>().localScale = new Vector3(0.2f,0.39f, 0.5982905f);
        }
        
    }
}
