using EventBrokerFolder;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cred._00_Game.Scripts.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
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
