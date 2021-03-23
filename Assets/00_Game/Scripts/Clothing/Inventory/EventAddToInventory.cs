namespace Clothing.Inventory {
    public class EventAddToInventory {
        public readonly CombinedWearables combinedWearable;
        public readonly int addOrSubtractAmount;

        public EventAddToInventory(CombinedWearables combinedWearable, int addOrSubtractAmount) {
            this.combinedWearable = combinedWearable;
            this.addOrSubtractAmount = addOrSubtractAmount;
        }
    }
}