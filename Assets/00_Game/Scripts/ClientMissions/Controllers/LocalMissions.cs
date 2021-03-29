using System.Collections.Generic;
using ClientMissions.Data;
using UnityEngine;

namespace ClientMissions.Controllers {
    public class LocalMissions : ISavedMission{
        const int MaxCurrentMissions = 3;
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
}