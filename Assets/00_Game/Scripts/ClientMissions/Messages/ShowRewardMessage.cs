namespace ClientMissions.Messages {
    public class ShowRewardMessage {
        public readonly int RewardPoints;

        public ShowRewardMessage(int reward) {
            RewardPoints = reward;
        }
    }
}