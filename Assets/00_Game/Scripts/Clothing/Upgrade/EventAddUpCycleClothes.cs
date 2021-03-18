using HUD.Clothing;

namespace Clothing.Upgrade {
    public class EventAddUpCycleClothes {
        public readonly Wearable wearable;
        public readonly InventoryButtonScript inventoryButtonScript;

        public EventAddUpCycleClothes(Wearable wearable, InventoryButtonScript inventoryButtonScript) {
            this.wearable = wearable;
            this.inventoryButtonScript = inventoryButtonScript;
        }
    }
}