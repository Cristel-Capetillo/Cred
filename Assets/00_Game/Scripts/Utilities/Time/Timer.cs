using System;
using UnityEngine;

namespace Utilities.Time {

    public class Timer : MonoBehaviour{
        
        DateTime startTime;
        TimeHandler timeHandler;
        TimeStamp timeStamp;

        public void Begin(string saveID) {
            timeStamp = new TimeStamp(saveID);
        }

        public int TimePassedSeconds  => (int) (timeHandler.GetTime() - startTime).TotalSeconds;
        public int TimePassedMinutes => TimePassedSeconds / 60;
        
        public Timer(TimeHandler timeHandler, string saveID) {
            this.timeHandler = timeHandler;
            timeStamp = new TimeStamp(saveID);
        }

        public void ResetMe() {
            startTime = timeHandler.GetTime();
            Debug.Log("Reset method from Timer Class : "+startTime);
        }
    }
}