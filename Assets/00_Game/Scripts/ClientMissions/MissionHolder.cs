using System;
using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.MissionMessages;
using Club;
using UnityEngine;
using Utilities;

namespace ClientMissions {
    public class MissionHolder : MonoBehaviour{
        [SerializeField] MissionButtonScript missionUiPrefab;
        [SerializeField] Transform contentParent;
        List<MissionData> missionData = new List<MissionData>();
        MissionData currentMission;
        List<MissionButtonScript> missionButtonScripts = new List<MissionButtonScript>();
        MissionGenerator missionGenerator;
        
        
        //TODO: Check time
        //TODO: Connect Broker event

        void Start(){
            missionGenerator = GetComponent<MissionGenerator>();
            CreateMissionData();
            InstantiateMissions();
            EventBroker.Instance().SubscribeMessage<SelectMissionMessage>(SelectMission);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<SelectMissionMessage>(SelectMission);
        }

        public void OnStartMission(){
            if (currentMission == null){
                Debug.LogWarning("CurrentMission is null!");
                return;
            }
            throw new Exception("Not implemented yet!");
        }
        //TODO: When a button is pressed
        public void CheckMissions(){
            CreateMissionData();
            SendMissionData();
        }
        public void RemoveMission(){
            throw new Exception("Not implemented yet!");
        }

        void SelectMission(SelectMissionMessage selectMissionMessage){
            currentMission = selectMissionMessage.missionData;
            print(currentMission.Difficulty.name);
        }
        void InstantiateMissions(){
            foreach (var mission in missionData){
                missionButtonScripts.Add(Instantiate(missionUiPrefab, contentParent));
            }
        }
        void SendMissionData(){
            for (var i = 0; i < missionButtonScripts.Count; i++){
                if (missionButtonScripts[i].MissionData == null){
                    missionButtonScripts[i].Setup(missionData[i]);
                }
            }
        }
        void CreateMissionData(){
            var missingMissionsCount = 3 - missionData.Count;
            if(missingMissionsCount <= 0)
                return;
            for (var i = 0; i < missingMissionsCount; i++){
                missionData.Add(missionGenerator.CreateMissionData());
            }
            print(missionData.Count);
        }
    }
}