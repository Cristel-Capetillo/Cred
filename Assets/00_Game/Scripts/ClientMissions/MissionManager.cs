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
    public class MissionManager : MonoBehaviour{
        //TODO: create unit tests, cleanup code and change to real data...
        //TODO: Remove/replace missions
        const int MaxCurrentMissions = 3;
        [SerializeField] MissionButtonScript missionUiPrefab;
        [SerializeField] Transform contentParent;
        [SerializeField] int missionTimerInSec = 60;
        public List<SavableMissionData> savableMissionData = new List<SavableMissionData>();
        MissionData currentMission;
        List<MissionButtonScript> missionButtonScripts = new List<MissionButtonScript>();
        IMissionHolder missionHolder;
        MissionInitializer missionInitializer;
        MissionGenerator missionGenerator;
        
        List<MissionData> activeMissions = new List<MissionData>();

        //ForTesting:
        public List<MissionData> ActiveMissions => activeMissions;//For testing...
        public MissionData CurrentMission => currentMission;


        IEnumerator Start(){
            missionInitializer = GetComponent<MissionInitializer>();
            missionHolder = missionInitializer.GetMissionHolder();
            missionGenerator = missionInitializer.CreateMissionGenerator();
            EventBroker.Instance().SubscribeMessage<SelectMissionMessage>(SelectMission);
            yield return new WaitForSeconds(1f);
            InstantiateMissionUI();
            CheckMissions();
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<SelectMissionMessage>(SelectMission);
        }

        public void RemoveMission(){
            if (currentMission == null){
                Debug.Log("CurrentMission == null");
                return;
            }
            var anyMissionToRemove = missionHolder.RemoveMission(currentMission.SavableMissionData);
            Debug.Log("Any mission to remove? " + anyMissionToRemove);
            CheckMissions();
        }

        public void OnStartMission(){
            if (currentMission == null){
                Debug.LogWarning("CurrentMission is null!");
                return;
            }
            EventBroker.Instance().SendMessage(new ActiveMissionMessage(currentMission));
            //TODO: Load dress up scene!
        }
        public void CheckMissions(){
            savableMissionData = TimeCheck(missionHolder.GetMissions());
            GenerateSavableMissions();
            activeMissions = InitializeMissions();
            SendMissionData(activeMissions);
        }

        List<MissionData> InitializeMissions(){
            return savableMissionData.Select(savedMissionData => missionInitializer.GetSavedMission(savedMissionData)).ToList();
        }

        void GenerateSavableMissions(){
            if (savableMissionData.Count >= missionHolder.MaxMissions) return;
            var missingMissions = missionHolder.MaxMissions - savableMissionData.Count;
            for (var i = 0; i < missingMissions; i++){
                var newMission = missionGenerator.GenerateSavableMissionData();
                missionHolder.AddMission(newMission);
                savableMissionData.Add(newMission);
            }
        }

        List<SavableMissionData> TimeCheck(List<SavableMissionData> savableMissionDatas){
            var dateTime = FindObjectOfType<TimeManager>().timeHandler.GetTime();
            var unixTimestamp = Helper.ToUnixTimestamp(dateTime);
            var tempList = savableMissionDatas.ToArray();
            foreach (var savableMission in tempList){
                if (unixTimestamp - savableMission.UnixUtcTimeStamp > missionTimerInSec){
                    missionHolder.RemoveMission(savableMission);
                    savableMissionDatas.Remove(savableMission);
                    Debug.Log(unixTimestamp - savableMission.UnixUtcTimeStamp + " < " + missionTimerInSec);
                }
            }
            return savableMissionDatas;
        }

        void SelectMission(SelectMissionMessage selectMissionMessage){
            currentMission = selectMissionMessage.missionData;
        }

        void InstantiateMissionUI(){
            for (var i = 0; i < MaxCurrentMissions; i++){
                missionButtonScripts.Add(Instantiate(missionUiPrefab, contentParent));
            }
        }
        void SendMissionData(List<MissionData> missionData){
            for (var i = 0; i < missionButtonScripts.Count; i++){
                missionButtonScripts[i].Setup(missionData[i]);
            }
        }
    }
}