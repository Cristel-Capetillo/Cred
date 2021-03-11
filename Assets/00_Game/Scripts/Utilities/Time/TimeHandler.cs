using System;

namespace Cred._00_Game.Scripts.Utilities.Time {
    public class TimeHandler {

        ITimeProvider timeProvider;
        
        public TimeHandler() {
            timeProvider = new RealTime();
        }

        public TimeHandler(ITimeProvider timeProvider) {
            this.timeProvider = timeProvider;
        }

        public DateTime GetTime() {
            return timeProvider.GetTime();
        }
        /// <summary>
        /// Returns if a specified amount of time has elapsed between time1 and time2
        /// </summary>
        /// <param name="timeReq"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public bool EnoughTimePassed(int timeReq, DateTime time1, DateTime time2) {
            return timeProvider.TimeDifference(time1, time2) >= timeReq;
        }
    }
}