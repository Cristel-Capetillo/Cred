using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Time;

namespace Ads {
    public class AdWatchButton : MonoBehaviour {
        [SerializeField] int durationBetweenAdWatches = 5;
        Button button;
        Timer timer;
        
        void Start() {
            button = GetComponent<Button>();
            timer = new Timer(new TimeHandler(), "lastAdWatched");
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAdWatched>(TimeStampAdWatched);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(TimeStampAdWatched);
        }
        
        void TimeStampAdWatched(EventAdWatched eventAdWatched) {
            if(!eventAdWatched.doubleMissionRewards) {
                button.interactable = false;
                timer.Reset();
            }
        }

        void Update() {
            button.interactable = timer.TimePassedSeconds >= durationBetweenAdWatches;
        }
    }
}