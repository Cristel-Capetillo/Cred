using System;
using System.Globalization;
using Cred._00_Game.Scripts.Utilities.Time;
using Cred.Scripts.SaveSystem;
using EventBrokerFolder;
using UnityEngine;
using UnityEngine.UI;

namespace Cred._00_Game.Scripts.Ads
{
    public class AdWatchButton : MonoBehaviour, ISavable{

        [SerializeField] int durationBetweenAdWatches = 5;
        TimeHandler timeHandler;
        Button button;
        DateTime lastAdWatchedTime;

        bool CanWatchAd => timeHandler.EnoughTimePassed(durationBetweenAdWatches, lastAdWatchedTime, timeHandler.GetTime());
        
        void Start() {
            timeHandler = new TimeHandler(new SystemTime());
            button = GetComponent<Button>();
            lastAdWatchedTime = DateTime.Now; //must get it from firebase
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAdWatched>(TimeStampAdWatched);
            InvokeRepeating(nameof(InteractableButton), 1f, 1f);
        }

        void InteractableButton() {
            if(!button.interactable)
                button.interactable = CanWatchAd;
        }

        void TimeStampAdWatched(EventAdWatched eventAdWatched) {
            button.interactable = false;
            lastAdWatchedTime = DateTime.Now;
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(TimeStampAdWatched);
            CancelInvoke(nameof(InteractableButton));
        }

        public object ToBeSaved() {
            return lastAdWatchedTime.ToString(CultureInfo.InvariantCulture);
        }

        public void OnLoad(object value) {
            lastAdWatchedTime = DateTime.ParseExact(value.ToString(),
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
        }
    }
}
