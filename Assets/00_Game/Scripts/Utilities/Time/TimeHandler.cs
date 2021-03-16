using System;

namespace Utilities.Time {
    public class TimeHandler {
        ITimeProvider timeProvider;

        public TimeHandler() {
            timeProvider = new RealTime();
            timeProvider.SyncTime();
        }

        public TimeHandler(ITimeProvider timeProvider) {
            this.timeProvider = timeProvider;
            timeProvider.SyncTime();
        }
        
        public DateTime GetTime() {
            return timeProvider.GetTime();
        }

        /// <summary>
        /// Returns if a specified amount of time has elapsed between time1 and time2
        /// </summary>
        // public bool EnoughTimePassed(int timeReq, DateTime time1, DateTime time2) {
        //     var timePassed = timeProvider.TimeDifference(time1, time2);
        //     Debug.Log("Time Passed: " + timePassed);
        //     var tmp = timePassed >= timeReq;
        //     return tmp;
        // }
    }
}