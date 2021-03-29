namespace ClientMissions.Messages{
    public class UpdateFollowersMessage{
        public readonly int amountToUpdate;
        public UpdateFollowersMessage(int amountToUpdate) {
            this.amountToUpdate = amountToUpdate;
        }
    }
}