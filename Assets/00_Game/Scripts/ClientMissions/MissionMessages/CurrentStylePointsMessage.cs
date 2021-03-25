namespace ClientMissions.MissionMessages{
    public class CurrentStylePointsMessage{
        public readonly int currentStylePoints;

        public CurrentStylePointsMessage(int currentStylePoints){
            this.currentStylePoints = currentStylePoints;
        }
    }
}