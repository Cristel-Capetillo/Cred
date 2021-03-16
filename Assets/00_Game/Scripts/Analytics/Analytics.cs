using System;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.UI;

namespace Analytics {
    public class Analytics : MonoBehaviour {
        // Start is called before the first frame update
        const string UserID = "UserFredrik";
        
        
        void Start() {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            FirebaseAnalytics.SetUserId(UserID);
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression);
            //FirebaseAnalytics.LogEvent(UserID, FirebaseAnalytics.ParameterValue, 1);
            UnityEngine.Analytics.Analytics.SendEvent("Test1", "para test", 1, "");
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression);
            }
        }
    }
}