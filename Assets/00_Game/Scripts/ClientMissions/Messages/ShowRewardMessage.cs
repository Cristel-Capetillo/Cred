namespace ClientMissions.Messages {
    public class ShowRewardMessage {
        public readonly int CurrencyReward;
        public readonly int FollowersReward;
        public ShowRewardMessage(int currencyReward, int followersReward) {
            CurrencyReward = currencyReward;
            FollowersReward = followersReward;
        }
    }
}