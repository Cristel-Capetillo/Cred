using System;
using SaveSystem;
using UnityEngine;

namespace Utilities.Time {
    public class TimeStamp : ISavable<string> {
        
        public static readonly DateTime epoch = new DateTime(1970, 01, 01, 0, 0, 0);

        SaveHandler saveHandler;
        string ID;

        public DateTime Time { get; private set; }

        /// <summary>
        /// Loads the timeStamp from Online Save System with the given parameter name.
        /// Call Save() to save to SaveHandler.
        /// </summary>
        public TimeStamp(string saveID) {
            saveHandler = new SaveHandler(saveID);
            ID = saveID;
            saveHandler.Load(this);
        }

        /// <summary>
        /// Create a new TimeStamp with the provided parameter name, and assign it a DateTime
        /// this will then save to the SaveHandler.
        /// </summary>
        public TimeStamp(string saveID, DateTime pointInTime) {
            saveHandler = new SaveHandler(saveID);
            ID = saveID;
            Time = pointInTime;
            Save();
        }
        /// <summary>
        /// Saves the timeStamp to the saveHandler
        /// </summary>
        public void Save() {
            saveHandler.Save(this);
        }

        public string ToBeSaved() {
            var savedTimeStamp = Convert.ToString(Time);
            Debug.Log($"Saved Timestamp {ID}: " + savedTimeStamp);
            return savedTimeStamp;
        }

        public void OnLoad(string value) {
            
            if (string.IsNullOrEmpty(value)) {
                Time = epoch;
                return;
            }
            Debug.Log($"Loaded Timestamp {ID}: " + value);
            Time = Convert.ToDateTime(value);
        }

        public override string ToString() {
            return Convert.ToString(Time);
        }
    }
}