namespace ClientMissions.Controllers{
    public interface IPlayer{
        public int Followers{ get; }
        public int MaxFollowers{ get; }
        public int MissionIndex{ get; set; }
        public int ClientIndex{ get; set; }
    }
}