using UnityEngine;
using UnityEngine.UI;

namespace LoginScene {
    public class ClickEvents : MonoBehaviour {
        public InputField usernameText;
        public InputField passwordText;
        public GameObject submitButton;
        public GameObject loginToggle;
        public GameObject signUpToggle;

        Vector3 btnScales;

        // Start is called before the first frame update
        void Start() {
            submitButton.GetComponent<Button>().onClick.AddListener(LoginClick);
            btnScales = loginToggle.GetComponent<RectTransform>().localScale;
        }

        public void LoginClick() {
            var username = usernameText.text;
            var password = passwordText.text;
            if (username == "" || password == "") {
                Debug.Log("Please enter both username and password");
                return;
            }

            StartCoroutine(GetComponent<DB>().Login(username, password));
        }

        public void SignUpClick() {
            var username = usernameText.text;
            var password = passwordText.text;
            if (username == "" || password == "") {
                Debug.Log("Please enter both username and password");
                return;
            }

            StartCoroutine(GetComponent<DB>().SignUp(username, password));
        }

        public void LoginToggleButton() {
            //change submit button style
            submitButton.transform.Find("Text").GetComponent<Text>().text = "Login";
            submitButton.GetComponent<Image>().color = new Color(1f, 0.19215686274f, 0.6862745098f, 1);

            //change submitbutton functionality
            submitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            submitButton.GetComponent<Button>().onClick.AddListener(LoginClick);

            //change toggle buttons style
            loginToggle.GetComponent<RectTransform>().localScale = btnScales;
            signUpToggle.GetComponent<RectTransform>().localScale = btnScales;
        }

        public void SignUpToggleButton() {
            submitButton.transform.Find("Text").GetComponent<Text>().text = "Sign Up";
            submitButton.GetComponent<Image>().color = new Color(0.16078431372f, 0.72549019607f, 0.98039215686f, 1);

            submitButton.GetComponent<Button>().onClick.RemoveAllListeners();
            submitButton.GetComponent<Button>().onClick.AddListener(SignUpClick);

            loginToggle.GetComponent<RectTransform>().localScale = btnScales;
            signUpToggle.GetComponent<RectTransform>().localScale = btnScales;
        }
    }
}