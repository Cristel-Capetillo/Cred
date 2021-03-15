using Core;
using UnityEngine;
using Utilities;

namespace HUD
{
    public class SwapScene : MonoBehaviour
    {
        public void LoadScene(string sceneToLoad) {
            EventBroker.Instance().SendMessage(new EventSceneLoad(sceneToLoad));
        }
    }
}
