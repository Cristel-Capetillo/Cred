using ClientMissions.Data;

namespace ClientMissions.Messages{
    public class SelectMissionMessage{
        public readonly MissionData MissionData;

        public SelectMissionMessage(MissionData missionData){
            MissionData = missionData;
        }
    }
}