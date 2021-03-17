using System;
using System.Globalization;
using SaveSystem;
using UnityEngine;

namespace Utilities.Time {
    /// <summary>
    /// Timer: tells us, how much time has passed since a certain Timestamp
    /// </summary>
    public class Timer : ISavable<string> {
        DateTime startTime;
        TimeHandler timeHandler;
        SaveHandler saveHandler;
        
        readonly DateTime epoch = new DateTime(1970, 01, 01, 0, 0, 0);
        
        public int TimePassedSeconds  => (int) (timeHandler.GetTime() - startTime).TotalSeconds;
        public int TimePassedMinutes => TimePassedSeconds / 60;
        
        public Timer(TimeHandler timeHandler, string saveID) {
            this.timeHandler = timeHandler;
            saveHandler = new SaveHandler(saveID);
            saveHandler.Load(this);
        }

        public void Reset() {
            startTime = timeHandler.GetTime();
            saveHandler.Save(this);
        }

        public string ToBeSaved() {
            var savedTimeStamp = Convert.ToString(startTime);
            Debug.Log("Saved : " +savedTimeStamp);
            return savedTimeStamp;
        }

        public void OnLoad(string value) {
            Debug.Log("Loaded : " +value);
            if (string.IsNullOrEmpty(value)) {
                startTime = epoch;
                return;
            }

            startTime = Convert.ToDateTime(value);
        }
    }
}