namespace Clothing.Inventory {
    public class EventShowReward {
        public CombinedWearables Reward;
        public readonly int textAmount;
        
        public EventShowReward(CombinedWearables reward) {
            this.Reward = reward;
        }

        public EventShowReward(CombinedWearables reward, int textAmount) {
            this.Reward = reward;
            this.textAmount = textAmount;
        }
    }
}