namespace Clothing.Upgrade {
    public class EventCoinsToSpend {
        public readonly int coins;
        public readonly int stylePoints;

        public EventCoinsToSpend(int coins, int stylePoints) {
            this.coins = coins;
            this.stylePoints = stylePoints;
        }
    }
}