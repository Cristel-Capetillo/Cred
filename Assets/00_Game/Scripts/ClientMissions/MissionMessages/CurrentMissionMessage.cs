using ClientMissions.Data;

namespace ClientMissions.MissionMessages{
    public class CurrentMissionMessage : SelectMissionMessage{
        public CurrentMissionMessage(MissionData missionData) : base(missionData){ }
    }
}