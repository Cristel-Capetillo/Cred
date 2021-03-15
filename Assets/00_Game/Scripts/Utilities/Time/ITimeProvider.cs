using System;

namespace Utilities.Time {
    public interface ITimeProvider {
        public DateTime GetTime();
        public int TimeDifference(DateTime time1, DateTime time2);
    }
}