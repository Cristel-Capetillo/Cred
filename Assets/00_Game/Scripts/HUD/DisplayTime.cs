using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities.Time;

public class DisplayTime : MonoBehaviour {
    
    TMP_Text timeDisplayTime;
    TimeManager timeManager;
    
    // Start is called before the first frame update
    void Start() {
        timeDisplayTime = GetComponent<TMP_Text>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update() {
        timeDisplayTime.text = timeManager.timeHandler.GetTime().ToString();
    }
}
