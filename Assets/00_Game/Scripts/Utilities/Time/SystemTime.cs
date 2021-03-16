using System;

namespace Utilities.Time {
    public class SystemTime : ITimeProvider {
        
        DateTime lastSyncTime;
        float unityTime;
        
        public DateTime GetTime() {
            return lastSyncTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup - unityTime);
        }

        public void SyncTime() {
            lastSyncTime = DateTime.Now; //Get this from webSource
            unityTime = UnityEngine.Time.realtimeSinceStartup;
        }
    }
}