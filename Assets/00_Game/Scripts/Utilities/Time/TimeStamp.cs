using System;
using SaveSystem;
using UnityEngine;

namespace Utilities.Time {
    public class TimeStamp : ISavable<string> {
        
        readonly DateTime epoch = new DateTime(1970, 01, 01, 0, 0, 0);
        
        SaveHandler saveHandler;
        
        public DateTime Time { get; private set; }

        public TimeStamp(string saveID) {
            saveHandler = new SaveHandler(saveID);
            saveHandler.Load(this);
        }

        public TimeStamp(string saveID, DateTime pointInTime) {
            saveHandler = new SaveHandler(saveID);
            Time = pointInTime;
            Save();
        }

        public void Save() {
            saveHandler.Save(this);
        }

        public string ToBeSaved() {
            var savedTimeStamp = Convert.ToString(Time);
            Debug.Log("Saved : " + savedTimeStamp);
            return savedTimeStamp;
        }

        public void OnLoad(string value) {
            Debug.Log("Loaded : " + value);
            if (string.IsNullOrEmpty(value)) {
                Time = epoch;
                return;
            }

            Time = Convert.ToDateTime(value);
        }

        public override string ToString() {
            return Convert.ToString(Time);
        }
    }
}