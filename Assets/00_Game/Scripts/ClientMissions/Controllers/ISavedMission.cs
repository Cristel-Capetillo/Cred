using System.Collections.Generic;
using ClientMissions.Data;

namespace ClientMissions.Controllers{
    public interface ISavedMission{
        int MaxMissions{ get; }
        public int MissionIndex{ get; set; }
        public int ClientIndex{ get; set; }
        bool AddMission(SavableMissionData savableMissionData);
        bool RemoveMission(SavableMissionData savableMissionData);
        List<SavableMissionData> GetMissions();
        
    }
}