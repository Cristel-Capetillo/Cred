using System.Collections.Generic;

namespace Clothing.Inventory {
    public class EventUpdatePlayerInventory {
        public readonly CombinedWearables combinedWearable;
        public readonly List<CombinedWearables> CombinedWearableses;
        public readonly int addOrSubtractAmount;

        public EventUpdatePlayerInventory(CombinedWearables combinedWearable, int addOrSubtractAmount) {
            this.combinedWearable = combinedWearable;
            this.addOrSubtractAmount = addOrSubtractAmount;
        }
        public EventUpdatePlayerInventory(List<CombinedWearables> combinedWearables, int addOrSubtractAmount){
            CombinedWearableses = combinedWearables;
            this.addOrSubtractAmount = addOrSubtractAmount;
        }
    }
}