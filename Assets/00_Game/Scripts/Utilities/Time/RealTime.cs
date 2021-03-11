using System;
using System.Globalization;
using System.Net;

namespace Cred._00_Game.Scripts.Utilities.Time {
    public class RealTime : ITimeProvider{
        
        public DateTime GetTime() {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();
            var todaysDates = response.Headers["date"];
            return DateTime.ParseExact(todaysDates,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal);
        }
        
        public int TimeDifference(DateTime time1, DateTime time2) {
            return time1.Subtract(time2).Minutes;
        }
    }
}