using System;
using System.Collections;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Time;

namespace Ads {
    public class AdWatchButton : MonoBehaviour {
        const string LastAdWatched = "lastAdWatched";

        [SerializeField] int durationBetweenAdWatches = 5;

        Button button;
        TimeManager timeManager;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAfterLoad>(AfterLoad);
            button = GetComponent<Button>();
            timeManager = FindObjectOfType<TimeManager>();
            timeManager.LastOccuredTime(LastAdWatched);
        }

        void AfterLoad(EventAfterLoad afterLoad) {
            if (!afterLoad.shouldRunAfterLoad) return;
            button.onClick.AddListener(() => FindObjectOfType<AdsManager>().ShowRewardedAd(false));
            button.interactable = timeManager.CanIPlay(durationBetweenAdWatches);

            if (!button.interactable) {
                StartCoroutine(timeManager
                    .OnComplete(timeManager.HowLongBeforeICan(LastAdWatched, durationBetweenAdWatches),
                        () => button.interactable = true));
            }
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAdWatched>(TimeStampAdWatched);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(TimeStampAdWatched);
            StopCoroutine(nameof(timeManager.OnComplete));
        }

        void TimeStampAdWatched(EventAdWatched eventAdWatched) {
            if (!eventAdWatched.doubleMissionRewards) {
                button.interactable = false;
                TimeStamp lastAdTimeStamp = new TimeStamp(LastAdWatched, timeManager.timeHandler.GetTime());
                lastAdTimeStamp.Save();
                StartCoroutine(timeManager.OnComplete(durationBetweenAdWatches, () => button.interactable = true));
            }
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                EventBroker.Instance().SendMessage(new EventAdWatched(false));
            }
        }
    }
}