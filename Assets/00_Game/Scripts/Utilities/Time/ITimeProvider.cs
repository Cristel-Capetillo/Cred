using System;

namespace Cred._00_Game.Scripts.Utilities.Time {
    public interface ITimeProvider {
        public DateTime GetTime();
        public int TimeDifference(DateTime time1, DateTime time2);

    }
}