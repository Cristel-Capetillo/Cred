using System;
using System.Globalization;
using System.Net;

namespace Utilities.Time {
    public class RealTime : ITimeProvider {
        
        DateTime lastSyncTime;
        float unityTime;
        
        public DateTime GetTime() {
            return lastSyncTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup - unityTime);
        }

        public void SyncTime() {
            lastSyncTime = ServerTime();
            unityTime = UnityEngine.Time.realtimeSinceStartup;
        }

        DateTime ServerTime() {
            var myHttpWebRequest = (HttpWebRequest) WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();
            var todaysDates = response.Headers["date"];
            return DateTime.ParseExact(todaysDates,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
        }
    }
}