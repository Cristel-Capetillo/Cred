namespace Clothing.Inventory {
    public class EventUpdatePlayerInventory {
        public readonly CombinedWearables combinedWearable;
        public readonly int addOrSubtractAmount;

        public EventUpdatePlayerInventory(CombinedWearables combinedWearable, int addOrSubtractAmount) {
            this.combinedWearable = combinedWearable;
            this.addOrSubtractAmount = addOrSubtractAmount;
        }
    }
}