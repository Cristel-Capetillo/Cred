using System;

namespace Utilities.Time {
    public interface ITimeProvider {
        public DateTime GetTime();
        public void SyncTime();
    }
}