using ClientMissions.Data;

namespace ClientMissions.Messages{
    public class SendActiveMissionMessage : SelectMissionMessage{
        public SendActiveMissionMessage(MissionData missionData) : base(missionData){ }
    }
}