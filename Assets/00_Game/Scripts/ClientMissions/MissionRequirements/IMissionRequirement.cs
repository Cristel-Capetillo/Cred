using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public interface IMissionRequirement{
        bool PassedRequirement(CombinedWearables combinedWearables);
        // SavableRequirementData ToSavable();
    }
}