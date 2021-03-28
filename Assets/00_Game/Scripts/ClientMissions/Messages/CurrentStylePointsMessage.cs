namespace ClientMissions.Messages{
    public class CurrentStylePointsMessage{
        public readonly int CurrentStylePoints;

        public CurrentStylePointsMessage(int currentStylePoints){
            CurrentStylePoints = currentStylePoints;
        }
    }
}