using System.Collections.Generic;
using ClientMissions.Data;

namespace ClientMissions.Controllers{
    public interface ISavedMission{
        int MaxMissions{ get; }
        bool AddMission(SavableMissionData savableMissionData);
        bool RemoveMission(SavableMissionData savableMissionData);
        List<SavableMissionData> GetMissions();
        
    }
}