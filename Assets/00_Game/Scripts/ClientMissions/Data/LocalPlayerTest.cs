using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data {
    [Serializable]
    public class LocalPlayerTest : IPlayer, IMissionHolder{
        const int MaxCurrentMissions = 3;
        [SerializeField] int followers;
        [SerializeField] int maxFollowers;
        [SerializeField] int testCoins;
        
        public int Followers => followers;

        public int MaxFollowers => maxFollowers;

        public int TestCoins => testCoins;
        public int MissionIndex{
            get => PlayerPrefs.GetInt("MissionIndex",0);
            set => PlayerPrefs.SetInt("MissionIndex", value);
        }

        public int ClientIndex{
            get => PlayerPrefs.GetInt("ClientIndex",0);
            set => PlayerPrefs.SetInt("ClientIndex", value);
        }

        public int MaxMissions => MaxCurrentMissions;

        public bool AddMission(SavableMissionData savableMissionData){
            for (var i = 0; i < MaxCurrentMissions; i++){
                if (PlayerPrefs.GetString($"PlayerMission({i})", "") == ""){
                    PlayerPrefs.SetString($"PlayerMission({i})", JsonUtility.ToJson(savableMissionData));
                    return true;
                }
            }
            return false;
        }
        public bool RemoveMission(SavableMissionData savableMissionData){
            var missionDataToJson = JsonUtility.ToJson(savableMissionData);
            for (var i = 0; i < MaxCurrentMissions; i++){
                if (PlayerPrefs.GetString($"PlayerMission({i})", "") == missionDataToJson){
                    PlayerPrefs.SetString($"PlayerMission({i})", "");
                    return true;
                }
            }
            return false;
        }
        public List<SavableMissionData> GetMissions(){
            var jSonMission = GetMissionsFromPlayerPrefs();
            return ReturnSavableMissionData(jSonMission);
        }

        static List<SavableMissionData> ReturnSavableMissionData(List<string> jSonMission){
            var savableMissionData = new List<SavableMissionData>();
            foreach (var mission in jSonMission){
                if (mission != "")
                    savableMissionData.Add(JsonUtility.FromJson<SavableMissionData>(mission));
            }
            return savableMissionData;
        }
        static List<string> GetMissionsFromPlayerPrefs(){
            var jSonMission = new List<string>();
            for (var i = 0; i < MaxCurrentMissions; i++){
                var missionInfo = PlayerPrefs.GetString($"PlayerMission({i})", "");
                if (missionInfo != "")
                    jSonMission.Add(missionInfo);
            }
            return jSonMission;
        }
    }

    public interface IPlayer{
        public int Followers{ get; }
        public int MaxFollowers{ get; }
        public int MissionIndex{ get; set; }
        public int ClientIndex{ get; set; }
        
    }

    public interface IMissionHolder{
        int MaxMissions{ get; }
        bool AddMission(SavableMissionData savableMissionData);
        bool RemoveMission(SavableMissionData savableMissionData);
        List<SavableMissionData> GetMissions();
        
    }
}