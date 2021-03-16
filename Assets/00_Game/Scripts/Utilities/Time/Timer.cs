using System;

namespace Utilities.Time {
    /// <summary>
    /// Timer: tells us, how much time has passed since a certain Timestamp
    /// </summary>
    public class Timer {
        DateTime startTime;
        TimeHandler timeHandler;
        
        public int TimePassedSeconds {
            get => (int)(timeHandler.GetTime() - startTime).TotalSeconds;
        }
        
        public Timer(DateTime startTime, TimeHandler timeHandler) {
            this.startTime = startTime;
            this.timeHandler = timeHandler;
        }

        void Reset() {
            
        }
    }
}