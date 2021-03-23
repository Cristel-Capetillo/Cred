using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public interface IMissionRequirement{
        bool PassedRequirement(Wearable wearable);
        // SavableRequirementData ToSavable();
    }
}