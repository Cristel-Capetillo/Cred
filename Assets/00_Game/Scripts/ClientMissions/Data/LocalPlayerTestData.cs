using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data {
    [Serializable]
    public class LocalPlayerTestData : IPlayer, IMissionHolder{
        [SerializeField] int followers;
        [SerializeField] int maxFollowers;
        [SerializeField] int testCoins;
        [SerializeField] List<string> missions;
        [SerializeField] int maxMissions;

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

        public int MaxMissions => maxMissions;

        public bool AddMission(SavableMissionData savableMissionData){
            for (var i = 0; i < maxMissions; i++){
                if (PlayerPrefs.GetString($"PlayerMission({i})", "") == ""){
                    PlayerPrefs.SetString($"PlayerMission({i})", JsonUtility.ToJson(savableMissionData));
                    return true;
                }
            }
            return false;
        }

        public bool RemoveMission(SavableMissionData savableMissionData){
            var missionDataToJson = JsonUtility.ToJson(savableMissionData);
            for (var i = 0; i < maxMissions; i++){
                if (PlayerPrefs.GetString($"PlayerMission({i})", "") == missionDataToJson){
                    PlayerPrefs.SetString($"PlayerMission({i})", "");
                    return true;
                }
            }
            return false;
        }

        public List<SavableMissionData> GetMissions(){
            missions.Clear();
            var savableMissionData = new List<SavableMissionData>();
            for (int i = 0; i < maxMissions; i++){
                var missionInfo = PlayerPrefs.GetString($"PlayerMission({i})", "");
                if(missionInfo != "") 
                    missions.Add(missionInfo);
            }
            foreach (var mission in missions){
                if(mission != "")
                    savableMissionData.Add(JsonUtility.FromJson<SavableMissionData>(mission));
            }
            return savableMissionData;
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