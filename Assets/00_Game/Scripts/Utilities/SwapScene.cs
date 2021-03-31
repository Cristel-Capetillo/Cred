using Core;
using UnityEngine;

namespace Utilities {
    public class SwapScene : MonoBehaviour {
        public void LoadScene(string sceneToLoad) {
            EventBroker.Instance().SendMessage(new EventSceneLoad(sceneToLoad));
        }
    }
}