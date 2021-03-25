using Clothing;

namespace ClientMissions.MissionMessages {
    public class EventWearableStylePoints {
        public readonly CombinedWearables combinedWearable;

        public EventWearableStylePoints(CombinedWearables combinedWearable) {
            this.combinedWearable = combinedWearable;
        }
    }
}