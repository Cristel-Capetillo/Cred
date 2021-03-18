namespace Currency.Coins {
    public class EventUpdateCoins {
        public readonly int amountToUpdate;

        public EventUpdateCoins(int amountToUpdate) {
            this.amountToUpdate = amountToUpdate;
        }
    }
}