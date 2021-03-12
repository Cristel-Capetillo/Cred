using System;

namespace Cred._00_Game.Scripts.Utilities.Time {
    public class SystemTime : ITimeProvider{
        public DateTime GetTime() {
            return DateTime.Now;
        }

        public int TimeDifference(DateTime time1, DateTime time2) {
            return time2.Subtract(time1).Seconds;
        }
    }
}