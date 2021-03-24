using HUD.Clothing;

namespace Clothing.Upgrade {
    public class EventAddUpCycleClothes {
        public readonly CombinedWearables combinedWearable;
        public readonly AssignCombinedWearableToUpCycle assignCombinedWearableToUpCycle;

        public EventAddUpCycleClothes(CombinedWearables combinedWearable, AssignCombinedWearableToUpCycle assignCombinedWearableToUpCycle) {
            this.combinedWearable = combinedWearable;
            this.assignCombinedWearableToUpCycle = assignCombinedWearableToUpCycle;
        }
    }
}