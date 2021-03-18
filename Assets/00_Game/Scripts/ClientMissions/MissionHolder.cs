using System.Collections.Generic;
using UnityEngine;

namespace Club {
    public class MissionHolder : MonoBehaviour{
        List<MissionData> missionData = new List<MissionData>();
        MissionGenerator missionGenerator;

        void Start(){
            missionGenerator = GetComponent<MissionGenerator>();
        }

        public void CreateMissionData(){
            missionGenerator.CreateMissionData();
        }
    }
}