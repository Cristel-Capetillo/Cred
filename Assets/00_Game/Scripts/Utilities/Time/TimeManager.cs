using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.Time {
    public class TimeManager : MonoBehaviour {
        
        public TimeHandler timeHandler { get; private set; }

        void Start() {
            timeHandler = new TimeHandler();
        }

        public bool CanIPlay(string ID, int cooldown) {
            var thisLastOccured = new TimeStamp(ID).Time;
            // Debug.Log($"{ID} last occured : {thisLastOccured}");
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