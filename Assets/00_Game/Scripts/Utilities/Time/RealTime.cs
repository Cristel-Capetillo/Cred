using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Utilities.Time {
    public class RealTime : ITimeProvider {
        
        DateTime lastSyncTime;
        float unityTime;

        public RealTime() {
            SyncTime();
        }
        
        public DateTime GetTime() {
            return lastSyncTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup - unityTime);
        }

        public async void SyncTime() {
            lastSyncTime = await ServerTime();
            unityTime = UnityEngine.Time.realtimeSinceStartup;
        }
        
        async Task<DateTime> ServerTime() {
            var myHttpWebRequest = (HttpWebRequest) WebRequest.Create("http://www.microsoft.com");
            var response = await myHttpWebRequest.GetResponseAsync();
            var todaysDates = response.Headers["date"];
            var currentTime = DateTime.ParseExact(todaysDates,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
            return currentTime;
        }
    }
}