using System;
using System.Collections;
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
        const int MaxCurrentMissions = 3;
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
        
        IEnumerator Start(){
            missionInitializer = GetComponent<MissionInitializer>();
            missionHolder = missionInitializer.GetMissionHolder();
            missionGenerator = missionInitializer.CreateMissionGenerator();
            EventBroker.Instance().SubscribeMessage<SelectMissionMessage>(SelectMission);
            yield return new WaitForSeconds(1);
            CheckMissions();
            InstantiateMissionUI();
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
        
        public void CheckMissions(){
            ClearLists();
            savableMissionData = missionHolder.GetMissions();
            TimeCheck();
            savableMissionData = missionHolder.GetMissions();
            GenerateMissions();
            InitializeMissions();
            SendMissionData();
        }

        void InitializeMissions(){
            foreach (var saveMissionData in savableMissionData){
                missionData.Add(missionInitializer.GetSavedMission(saveMissionData));
            }
        }

        void GenerateMissions(){
            if (savableMissionData.Count >= missionHolder.MaxMissions) return;
            var missingMissions = missionHolder.MaxMissions - savableMissionData.Count;
            for (var i = 0; i < missingMissions; i++){
                var newMission = missionGenerator.GenerateSavableMissionData();
                missionHolder.AddMission(newMission);
            }
        }

        void ClearLists(){
            savableMissionData.Clear();
            missionData.Clear();
        }

        void TimeCheck(){
            var dateTime = FindObjectOfType<TimeManager>().timeHandler.GetTime();
            var unixTimestamp = Helper.ToUnixTimestamp(dateTime);
            var tempList = savableMissionData.ToArray();
            foreach (var savableMission in tempList){
                if (unixTimestamp - savableMission.UnixUtcTimeStamp > missionTimerInSec){
                    missionHolder.RemoveMission(savableMission);
                    Debug.Log(unixTimestamp - savableMission.UnixUtcTimeStamp + " < " + missionTimerInSec);
                }
            }
        }

        void SelectMission(SelectMissionMessage selectMissionMessage){
            currentMission = selectMissionMessage.missionData;
        }

        void InstantiateMissionUI(){
            for (var i = 0; i < MaxCurrentMissions; i++){
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