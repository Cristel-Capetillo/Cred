using System;
using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.Helpers;
using ClientMissions.MissionMessages;
using UnityEngine;
using Utilities;
using Utilities.Time;

namespace ClientMissions {
    public class MissionHolder : MonoBehaviour{
        //TODO: create unit tests, cleanup code and change to real data...
        //TODO: Remove/replace missions
        [SerializeField] MissionButtonScript missionUiPrefab;
        [SerializeField] Transform contentParent;
        [SerializeField] int missionTimerInSec = 60;
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

        public void RemoveMission(){
            if (!missionHolder.RemoveMission(currentMission.SavableMissionData))
                return;
            RemoveMissionData(currentMission);
            CheckMissions();
        }

        public void OnStartMission(){
            if (currentMission == null){
                Debug.LogWarning("CurrentMission is null!");
                return;
            }
            EventBroker.Instance().SendMessage(new CurrentMissionMessage(currentMission));
            //TODO: Load dress up scene!
        }

        //TODO: Cleanup...!!!!!!!!!!!!!
        public void CheckMissions(){
            savableMissionData.Clear();
            missionData.Clear();
            savableMissionData = missionHolder.GetMissions();

            var dateTime = FindObjectOfType<TimeManager>().timeHandler.GetTime();
            var unixTimestamp = Helper.ToUnixTimestamp(dateTime);
            foreach (var savableMission in savableMissionData){
                if (unixTimestamp - savableMission.UnixUtcTimeStamp > 60)
                    missionHolder.RemoveMission(savableMission);
            }
            
            if (savableMissionData.Count < missionHolder.MaxMissions){
                var missingMissions = missionHolder.MaxMissions - savableMissionData.Count;
                for (var i = 0; i < missingMissions; i++){
                    var newMission = missionGenerator.GenerateSavableMissionData();
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
        }

        void InstantiateMissionUI(){
            foreach (var mission in missionData){
                missionButtonScripts.Add(Instantiate(missionUiPrefab, contentParent));
            }
        }

        void RemoveMissionData(MissionData missionData){
            foreach (var missionButtonScript in missionButtonScripts.Where(missionButtonScript => missionButtonScript.MissionData == missionData)){
                missionButtonScript.Setup(null);
                return;
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