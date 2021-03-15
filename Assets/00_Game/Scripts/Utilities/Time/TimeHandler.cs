using System;
using UnityEngine;

namespace Utilities.Time {
    public class TimeHandler {

        ITimeProvider timeProvider;
        
        public TimeHandler() {
            timeProvider = new RealTime();
        }

        public TimeHandler(ITimeProvider timeProvider) {
            this.timeProvider = timeProvider;
        }

        public DateTime GetTime() {
            var tmp = timeProvider.GetTime();
            Debug.Log(tmp);
            return tmp;
        }
        
        /// <summary>
        /// Returns if a specified amount of time has elapsed between time1 and time2
        /// </summary>
        public bool EnoughTimePassed(int timeReq, DateTime time1, DateTime time2) {
            var timePassed = timeProvider.TimeDifference(time1, time2);
            Debug.Log("Time Passed: " +timePassed);
            var tmp = timePassed >= timeReq;
            return tmp;
        }
    }
}