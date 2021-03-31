using System.Collections;
using UnityEngine;
using Utilities;

namespace Login {
    public class SaveUserAgreement : MonoBehaviour {
        public GameObject userAgreementObject;
        SwapScene swapScene;

        void Start() {
            swapScene = GetComponent<SwapScene>();
            if (CheckIfUserAgreementAccepted() == 1) StartCoroutine(LoadMainScene());
        }

        public void AcceptUserAgreementAndChangeScene() {
            SaveUserAgreementOnClick();
            StartCoroutine(LoadMainScene());
            SavePlayerPrefsToDisc();
        }

        static void SaveUserAgreementOnClick() {
            PlayerPrefs.SetInt("keyName", 1);
        }

        static int CheckIfUserAgreementAccepted() {
            return PlayerPrefs.GetInt("keyName", 0);
        }

        static void SavePlayerPrefsToDisc() {
            PlayerPrefs.Save();
        }

        IEnumerator LoadMainScene() {
            userAgreementObject.SetActive(false);
            yield return new WaitForSeconds(2);
            swapScene.LoadScene("MainScene");
        }
    }
}