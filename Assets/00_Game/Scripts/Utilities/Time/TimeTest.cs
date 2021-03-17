using System;
using UnityEngine;

namespace Utilities.Time {
    public class TimeTest : MonoBehaviour {

        
        DateTime tmp = default;
        Timer timer;
        
        void Start() {
            timer = new Timer(new TimeHandler(), "lastAdWatched");
            Debug.Log(timer.TimePassedSeconds);
        }
    }
}