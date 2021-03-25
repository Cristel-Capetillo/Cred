using HUD.Clothing;

namespace Clothing.Upgrade {
    public class EventAddToUpgradeSlot {
        public readonly CombinedWearables combinedWearable;

        public EventAddToUpgradeSlot(CombinedWearables combinedWearable) {
            this.combinedWearable = combinedWearable;
        }
    }
}