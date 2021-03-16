using System;
using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace Club {
    public class MissionController : MonoBehaviour {

        [SerializeField] List<MissionData> missionList = new List<MissionData>();
        [SerializeField] PlayerData playerData;
        [SerializeField] List<Wearable> wearables = new List<Wearable>();

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                StartMission();
            }
        }

        public void StartMission() {
            var currentMission = missionList[playerData.CurrentMissionIndex];
            Debug.Log(currentMission.name);
            // send event to UI
        }

        
        
        /*// calculate how many style points this mission requires
        // Based on a set amount of maximum followers that can be reached.
        
        public int CalculateStylePoints(int followers, int maxFollowers) {
            var t = Mathf.InverseLerp(0, maxFollowers, followers);
            return Mathf.RoundToInt(Mathf.Lerp(1, maxStylePoints,t)); 
        }

        // calculate the percentage of the current selected style.
        
        public float CalculateStylePercentage(int stylePoints, float currentMissionStylePoints) {
            return stylePoints / currentMissionStylePoints;
        }
        
        // calculate the total reward the user gets from that style percentage
        public int CalculateTotalReward(float stylePercentage){
            return (int) (stylePercentage * maxReward);
        }*/
    }
}