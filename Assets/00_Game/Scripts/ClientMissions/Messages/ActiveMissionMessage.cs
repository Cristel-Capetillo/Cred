using ClientMissions.Data;

namespace ClientMissions.Messages{
    public class ActiveMissionMessage : SelectMissionMessage{
        public ActiveMissionMessage(MissionData missionData) : base(missionData){ }
    }
}