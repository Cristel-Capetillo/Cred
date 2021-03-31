using TMPro;
using UnityEngine;
using Utilities.Time;

namespace HUD {
    public class DisplayTime : MonoBehaviour {
        TMP_Text timeDisplayTime;
        TimeManager timeManager;

        void Start() {
            timeDisplayTime = GetComponent<TMP_Text>();
            timeManager = FindObjectOfType<TimeManager>();
        }

        void Update() {
            timeDisplayTime.text = timeManager.timeHandler.GetTime().ToString();
        }
    }
}