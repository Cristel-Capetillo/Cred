using System;

namespace Utilities.Time {
    public class TimeHandler {
        readonly ITimeProvider timeProvider;

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
    }
}