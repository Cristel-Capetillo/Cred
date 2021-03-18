using System.Collections.Generic;
using UnityEngine;

namespace Club {
    public class MissionHolder : MonoBehaviour{
        [SerializeField] MissionButtonScript missionUiPrefab;
        [SerializeField] Transform contentParent;
        List<MissionData> missionData = new List<MissionData>();
        MissionGenerator missionGenerator;
        
        
        //TODO: Check time
        //TODO: Connect Broker event

        void Start(){
            missionGenerator = GetComponent<MissionGenerator>();
            CreateMissionData();
        }
        //TODO: When a button is pressed
        public void CheckMissions(){
            if(!CheckTime())
                return;
            CreateMissionData();
        }
        public void RemoveMission(MissionData currentMissionData){
            missionData.Remove(currentMissionData);
        }
        public void InstantiateMissions(){
            foreach (var mission in missionData){
                var instance = Instantiate(missionUiPrefab, contentParent);
                instance.Setup(mission);
            }
        }
        bool CheckTime(){
            return true;
        }
        void CreateMissionData(){
            var missingMissionsCount = 3 - missionData.Count;
            if(missingMissionsCount <= 0)
                return;
            for (int i = 0; i < missingMissionsCount; i++){
                missionData.Add(missionGenerator.CreateMissionData());
            }
            print(missionData.Count);
        }
    }
}