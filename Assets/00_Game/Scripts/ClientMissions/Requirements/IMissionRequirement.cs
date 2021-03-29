using Clothing;

namespace ClientMissions.Requirements{
    public interface IMissionRequirement{
        bool PassedRequirement(CombinedWearables combinedWearables);
    }
}