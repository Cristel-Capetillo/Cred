namespace ClientMissions.MissionMessages {
    public class EventUpdateStylePoints {
        public readonly int CurrentStylePoints;

        public EventUpdateStylePoints(int currentStylePoints) {
            CurrentStylePoints = currentStylePoints;
        }
    }
}