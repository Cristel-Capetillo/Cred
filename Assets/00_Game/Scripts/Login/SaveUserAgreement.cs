using UnityEngine;

namespace Login {
    public class SaveUserAgreement : MonoBehaviour {
        void Start() {
            //Fetch the PlayerPref settings
            LoadUserAgreement();
        }

        public void HasKey(string keyName) {
            if (PlayerPrefs.HasKey(keyName))
                Debug.Log("[SaveUserAgreement_HasKey]\nThe key " + keyName + " exists");
            else
                Debug.Log("[SaveUserAgreement_HasKey]\nThe key " + keyName + " does not exist");
        }

        public void SetInt(string keyName, int value) {
            PlayerPrefs.SetInt(keyName, value);
        }

        public int GetInt(string keyName) {
            return PlayerPrefs.GetInt(keyName);
        }

        public void SaveUserAgreementOnClick() {
            //Fetch the score from the PlayerPrefs (set these PlayerPrefs in another script). If no Int of this name exists, the default is 0.
            PlayerPrefs.SetInt("keyName", 1);
            //print("key"+keyName+value);
            //Debug.Log(keyName);
        }

        void LoadUserAgreement() {
            //Fetch the score from the PlayerPrefs (set these Playerprefs in another script). If no Int of this name exists, the default is 0.
            var userAgreementInt = PlayerPrefs.GetInt("keyName", 0);
            // print("key"+keyName+value);
            // Debug.Log(keyName);
        }

        public static void Save() {
            PlayerPrefs.Save();
        }
    }
}