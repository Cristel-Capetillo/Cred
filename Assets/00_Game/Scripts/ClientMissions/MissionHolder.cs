using System;
using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using Utilities;

namespace ClientMissions {
    public class MissionHolder : MonoBehaviour{
        //TODO: create unit tests, cleanup code and change to real data...
        //TODO: Remove/replace missions
        [SerializeField] MissionButtonScript missionUiPrefab;
        [SerializeField] Transform contentParent;
        List<MissionData> missionData = new List<MissionData>();
        List<SavableMissionData> savableMissionData = new List<SavableMissionData>();
        MissionData currentMission;
        List<MissionButtonScript> missionButtonScripts = new List<MissionButtonScript>();
        IMissionHolder missionHolder;
        MissionInitializer missionInitializer;
        MissionGenerator missionGenerator;

        void Start(){
            missionInitializer = GetComponent<MissionInitializer>();
            missionHolder = missionInitializer.GetMissionHolder();
            missionGenerator = missionInitializer.CreateMissionGenerator();
            
            CheckMissions();
            InstantiateMissionUI();
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
        //TODO: Cleanup...
        public void CheckMissions(){
            savableMissionData.Clear();
            savableMissionData = missionHolder.GetMissions();
            if (savableMissionData.Count < missionHolder.MaxMissions){
                var missingMissions = missionHolder.MaxMissions - savableMissionData.Count;
                for (var i = 0; i < missingMissions; i++){
                    var newMission = missionGenerator.GenerateSavableMissionData();
                    missionGenerator.CycleIndex();
                    missionHolder.AddMission(newMission);
                }
            }
            savableMissionData = missionHolder.GetMissions();
            foreach (var saveMissionData in savableMissionData){
                missionData.Add(missionInitializer.GetSavedMission(saveMissionData));
            }
            SendMissionData();
        }
        void SelectMission(SelectMissionMessage selectMissionMessage){
            currentMission = selectMissionMessage.missionData;
            print(currentMission.Difficulty.name);
        }
        void InstantiateMissionUI(){
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
    }
}