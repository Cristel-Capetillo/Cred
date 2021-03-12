using System.Collections;
using System.Collections.Generic;
using Cred._00_Game.Scripts.SceneLoader;
using EventBrokerFolder;
using UnityEngine;

namespace Cred
{
    public class SwapScene : MonoBehaviour
    {
        public void LoadScene(string sceneToLoad) {
            EventBroker.Instance().SendMessage(new EventSceneLoad(sceneToLoad));
        }
    }
}
