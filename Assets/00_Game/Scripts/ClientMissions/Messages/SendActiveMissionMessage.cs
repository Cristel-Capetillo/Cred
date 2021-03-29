using System.Collections.Generic;
using ClientMissions.Data;
using Clothing;

namespace ClientMissions.Messages{
    public class SendActiveMissionMessage{
        public readonly MissionData MissionData;
        public readonly Dictionary<ClothingType, CombinedWearables> CurrentWearables;
        public SendActiveMissionMessage(MissionData missionData, Dictionary<ClothingType, CombinedWearables> currentWearables){
            MissionData = missionData;
            CurrentWearables = currentWearables;
        }
    }
}