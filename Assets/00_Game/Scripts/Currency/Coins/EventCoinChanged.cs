namespace Currency.Coins {
    public class EventCoinChanged {
        public readonly long Coins;

        public EventCoinChanged(long coins) {
            Coins = coins;
        }
    }
}