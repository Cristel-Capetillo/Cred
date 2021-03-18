using System;
using System.Collections.Generic;
using System.Linq;
using ClientMissions;
using ClientMissions.Data;
using ClientMissions.MissionMessages;
using Clothing;
using UnityEngine;
using Utilities;

namespace Club {
    public class MissionController : MonoBehaviour {

        [SerializeField] List<MissionData> missionList = new List<MissionData>();
        [SerializeField] PlayerData playerData;
        [SerializeField] List<Wearable> wearables = new List<Wearable>();//TODO: test remove this
        [SerializeField] int maxFollowers = 1000;
        [SerializeField] int maxStylePoints = 50;
        List<Wearable> wearableList = new List<Wearable>();
        int _currentMinStylepointValue;
        int _currentMaxStylepointValue;
        int _currentStylePoints;
        MissionData _currentMission;
        void Start(){
            // EventBroker.Instance().SubscribeMessage<MissionWearableMessage>(GetWearableList);
            //StartMission();
            _currentStylePoints = GetStylePoints();
            //EndMission();
        }
        // void OnDestroy(){
        //     EventBroker.Instance().UnsubscribeMessage<MissionWearableMessage>(GetWearableList);
        // }

        /*public void StartMission() {
            _currentMission = missionList[playerData.CurrentMissionIndex];
            (_currentMinStylepointValue, _currentMaxStylepointValue) =
                AdjustStylePoint(_currentMission.MinimumStylePoints, _currentMission.MaximumStylePoints);
            print($"Min: {_currentMinStylepointValue} Max: {_currentMaxStylepointValue}");
        }*/
        // public void EndMission(){
        //     print("Colors match: " + CheckColors());
        //     if (_currentStylePoints < _currentMinStylepointValue){
        //         print("Mission failed!");
        //         return;
        //     }
        //     print(CalculateReword());
        // }

        //TODO: Get equation from GameDesigner! 
        /*int CalculateReword(){
            var t = Mathf.InverseLerp(0, _currentMaxStylepointValue, _currentStylePoints);
            return Mathf.RoundToInt(Mathf.Lerp(_currentMinStylepointValue, _currentMaxStylepointValue + _currentMission.MaxReward, t));
        }*/
        int GetStylePoints(){
            return wearables.Sum(wearable => wearable.StylePoints);
        }

        //bool CheckColors(){
        //    return wearables.Any(wearable => wearable.ColorData.Any(colorData => _currentMission.RequiredColors.Contains(colorData)));
        //}
        void GetWearableList(MissionWearableMessage missionWearableMessage){
            wearableList = missionWearableMessage.wearables;
        }
        (int, int) AdjustStylePoint(int minValue, int maxValue){
            var t = Mathf.InverseLerp(0, maxFollowers, playerData.Followers);
            minValue = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, t));
            maxValue = Mathf.RoundToInt(Mathf.Lerp(maxValue, maxStylePoints,t)); 
            return (minValue, maxValue);
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