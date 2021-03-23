namespace Clothing.Inventory {
    public class EventAddToInventory {
        public readonly Wearable wearable;
        public readonly int addOrSubtractAmount;

        public EventAddToInventory(Wearable wearable, int addOrSubtractAmount) {
            this.wearable = wearable;
            this.addOrSubtractAmount = addOrSubtractAmount;
        }
    }
}