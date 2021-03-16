using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Time;

namespace Ads {
    public class AdWatchButton : MonoBehaviour {
        [SerializeField] int durationBetweenAdWatches = 5;
        Button button;
        Timer timer;
        
        //bool CanWatchAd => timeHandler.EnoughTimePassed(durationBetweenAdWatches, lastAdWatchedTime, timeHandler.GetTime());

        void Start() {
            button = GetComponent<Button>();
            button.onClick.AddListener(FindObjectOfType<AdsManager>().ShowRewardedAd);
            timer = new Timer(new TimeHandler(), "lastAdWatched");
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAdWatched>(TimeStampAdWatched);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(TimeStampAdWatched);
        }
        
        void TimeStampAdWatched(EventAdWatched eventAdWatched) {
             button.interactable = false;
             timer.Reset();
        }

        void Update() {
            button.interactable = timer.TimePassedSeconds >= durationBetweenAdWatches;
        }
    }
}