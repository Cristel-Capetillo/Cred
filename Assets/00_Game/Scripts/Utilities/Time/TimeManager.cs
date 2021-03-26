using System;
using System.Collections;
using UnityEngine;

namespace Utilities.Time {
    public class TimeManager : MonoBehaviour {
        public TimeHandler timeHandler { get; private set; }
        TimeStamp timeStamp;
        [SerializeField] GameObject cooldownTimer;

        void Start() {
            timeHandler = new TimeHandler();
        }

        public void LastOccuredTime(string ID) {
            timeStamp = new TimeStamp(ID);
        }

        public bool CanIPlay(int cooldown) {
            var thisLastOccured = timeStamp.Time;
            return timeHandler.GetTime() >= thisLastOccured.AddSeconds(cooldown);
        }

        public int TimeRemaining(DateTime destinationTime) {
            return destinationTime.Subtract(timeHandler.GetTime()).Seconds;
        }

        public int HowLongBeforeICan(int cooldown) {
            //var thisLastOccured = new TimeStamp(ID);
            return (timeStamp.Time.AddSeconds(cooldown) - timeHandler.GetTime()).Seconds;
        }
        
        public int SecondsTillDateTime(DateTime when) {
            return when.Subtract(timeHandler.GetTime()).Seconds;
        }

        public bool IsThisToday(DateTime toCompare) {
            var atm = timeHandler.GetTime();
            return toCompare.DayOfYear == atm.DayOfYear && toCompare.Year == atm.Year;
        }
        
        public IEnumerator OnComplete(float delay, Action onComplete) {
            yield return new WaitForSeconds(delay);
            onComplete.Invoke();
        }
    }
}