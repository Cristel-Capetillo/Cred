namespace ClientMissions.MissionMessages {
    public class EventShowReward {
        public readonly int rewardPoints;

        public EventShowReward(int reward) {
            rewardPoints = reward;
        }
    }
}