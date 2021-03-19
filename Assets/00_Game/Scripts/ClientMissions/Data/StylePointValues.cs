namespace ClientMissions.Data{
    public class StylePointValues {
        public int MinStylePoints { get; private set; }
        public int MaxStylePoints { get; private set; }

        public StylePointValues(int minStylePoints, int maxStylePoints) {
            MinStylePoints = minStylePoints;
            MaxStylePoints = maxStylePoints;
        }
    }
}