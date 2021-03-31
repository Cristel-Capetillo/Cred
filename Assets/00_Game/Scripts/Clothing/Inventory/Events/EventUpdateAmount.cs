namespace Clothing.Inventory {
    public class EventUpdateAmount {
        public readonly int amount;
        public readonly string id;
        public EventUpdateAmount(string id, int amount) {
            this.id = id;
            this.amount = amount;
        }
    }
}