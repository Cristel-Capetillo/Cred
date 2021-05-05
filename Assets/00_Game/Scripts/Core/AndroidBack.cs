using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core {
    public class AndroidBack : MonoBehaviour{
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainScene")) {
                    EventBroker.Instance().SendMessage(new EventSceneLoad("MainScene"));
                }
                else {
                    Application.Quit();
                }
            }
        }
    }
}