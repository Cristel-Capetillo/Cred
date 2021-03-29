namespace ClientMissions.Messages{
    public class UpdateUIFollowersMessage{
        public readonly int Followers;

        public UpdateUIFollowersMessage(int followers){
            Followers = followers;
        }
    }
}