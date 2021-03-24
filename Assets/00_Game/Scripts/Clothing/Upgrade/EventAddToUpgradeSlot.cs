using HUD.Clothing;

namespace Clothing.Upgrade {
    public class EventAddToUpgradeSlot {
        public readonly CombinedWearables combinedWearable;
        public readonly InventoryButtonScript inventoryButtonScript;

        public EventAddToUpgradeSlot(CombinedWearables combinedWearable, AssignCombinedWearableToUpCycle assignCombinedWearableToUpCycle) {
            this.combinedWearable = combinedWearable;
            this.inventoryButtonScript = inventoryButtonScript;
        }
    }
}