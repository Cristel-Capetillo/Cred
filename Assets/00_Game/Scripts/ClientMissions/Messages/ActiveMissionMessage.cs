using ClientMissions.Data;

namespace ClientMissions.Messages{
    public class ActiveMissionMessage{
        public readonly MissionData MissionData;
        public ActiveMissionMessage(MissionData missionData){
            MissionData = missionData;
        }
    }
}