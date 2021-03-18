using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core {
    public class SceneLoader : MonoBehaviour {
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSceneLoad>(LoadScene);
        }

        void LoadScene(EventSceneLoad sceneEvent) {
            StartCoroutine(LoadSceneAsync(sceneEvent.sceneToLoad));
        }

        IEnumerator LoadSceneAsync(string sceneToLoad) {
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            EventBroker.Instance().SendMessage(new EventSceneSwap(true));
        }

        public void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventSceneLoad>(LoadScene);
        }
    }
}