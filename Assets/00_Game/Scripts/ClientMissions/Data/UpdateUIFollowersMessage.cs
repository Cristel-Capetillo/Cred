namespace ClientMissions.Data{
    public class UpdateUIFollowersMessage{
        public readonly int Followers;
        public readonly int MaxFollowers;
        public readonly int MinFollowers;

        public UpdateUIFollowersMessage(int followers, int maxFollowers, int minFollowers){
            Followers = followers;
            MaxFollowers = maxFollowers;
            MinFollowers = minFollowers;
        }
    }
}