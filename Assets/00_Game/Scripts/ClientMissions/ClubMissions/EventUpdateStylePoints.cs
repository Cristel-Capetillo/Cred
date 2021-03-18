namespace ClientMissions.ClubMissions {
    public class EventUpdateStylePoints {
        public readonly int currentStylePoints;
        public readonly ClubData clubData;

        public EventUpdateStylePoints(int currentStylePoints, ClubData clubData) {
            this.currentStylePoints = currentStylePoints;
            this.clubData = clubData;
        }
    }
}