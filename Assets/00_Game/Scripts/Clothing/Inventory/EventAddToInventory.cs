namespace Clothing.Inventory {
    public class EventAddToInventory {
        public readonly Wearable wearable;

        public EventAddToInventory(Wearable wearable) {
            this.wearable = wearable;
        }
    }
}