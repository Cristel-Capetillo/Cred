using System;
using System.Collections;
using System.Threading.Tasks;
using SaveSystem;
using UnityEngine;

namespace Utilities.Time {
    public class TimeManager : MonoBehaviour {
        public TimeHandler timeHandler { get; private set; }
        TimeStamp timeStamp;

        void Start() {
            timeHandler = new TimeHandler();
        }

        public void LastOccuredTime(string ID) {
            timeStamp = new TimeStamp(ID);
        }

        public bool CanIPlay(int cooldown) {
            var thisLastOccured = timeStamp.Time;
            Debug.Log($"last occured : {thisLastOccured}");

            if (thisLastOccured.AddSeconds(cooldown) >= timeHandler.GetTime()) {
                return true;
            }

            return false;
        }

        public int HowLongBeforeICan(string ID, int cooldown) {
            var thisLastOccured = new TimeStamp(ID);
            return (thisLastOccured.Time.AddSeconds(cooldown) - timeHandler.GetTime()).Seconds;
        }

        public IEnumerator OnComplete(float delay, Action onComplete) {
            yield return new WaitForSeconds(delay);
            onComplete.Invoke();
        }
    }
}