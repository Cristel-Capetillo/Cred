using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Club {
    public class MissionHolder : MonoBehaviour {
        List<MissionData> missionData = new List<MissionData>();
        const int maxMissions = 3;
        public void PopulateMissionData() {
            if (!missionData.Any()) {
                for (int i = 0; i <= maxMissions; i++) {
                    
                }
            }
        }
    }
}