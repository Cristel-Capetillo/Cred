using HUD.Clothing;

namespace Clothing.Upgrade {
    public class EventAddUpCycleClothes {
        public readonly CombinedWearables combinedWearable;
        public readonly InventoryButtonScript inventoryButtonScript;

        public EventAddUpCycleClothes(CombinedWearables combinedWearable, InventoryButtonScript inventoryButtonScript) {
            this.combinedWearable = combinedWearable;
            this.inventoryButtonScript = inventoryButtonScript;
        }
    }
}