namespace Clothing {
    public class EventClothesChanged {
        public CombinedWearables CombinedWearables { get; }

        public EventClothesChanged(CombinedWearables combinedWearables) {
            CombinedWearables = combinedWearables;
        }
    }
}