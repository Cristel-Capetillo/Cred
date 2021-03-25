using ClientMissions.Data;

namespace ClientMissions.MissionMessages{
    public class ActiveMissionMessage : SelectMissionMessage{
        public ActiveMissionMessage(MissionData missionData) : base(missionData){ }
    }
}