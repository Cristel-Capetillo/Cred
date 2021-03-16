using System;
using UnityEngine;

namespace Utilities.Time {
    public class TimeTest : MonoBehaviour {

        DateTime lastAdWatched = new DateTime(2021, 03, 16, 9, 0, 0);
        DateTime tmp = default;
        Timer timer;
        
        void Start() {
            timer = new Timer(lastAdWatched, new TimeHandler());
            Debug.Log(timer.TimePassedSeconds);
        }
    }
}