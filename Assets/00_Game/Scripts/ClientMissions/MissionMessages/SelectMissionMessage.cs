using ClientMissions.Data;

namespace ClientMissions.MissionMessages{
    public class SelectMissionMessage{
        public readonly MissionData missionData;

        public SelectMissionMessage(MissionData missionData){
            this.missionData = missionData;
        }
    }
}