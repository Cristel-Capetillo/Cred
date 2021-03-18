using System;
using System.Threading.Tasks;

namespace Utilities.Time {
    public class TimeHandler {
        readonly ITimeProvider timeProvider;

        public TimeHandler() {
            timeProvider = new RealTime();
        }

        public TimeHandler(ITimeProvider timeProvider) {
            this.timeProvider = timeProvider;
        }
        
        public DateTime GetTime() {
            return timeProvider.GetTime();
        }
    }
}