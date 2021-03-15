using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core {
    public class SceneLoader : MonoBehaviour {
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSceneLoad>(LoadScene);
        }

        void LoadScene(EventSceneLoad sceneEvent) {
            SceneManager.LoadScene(sceneEvent.Name);
        }

        public void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventSceneLoad>(LoadScene);
        }
    }
}