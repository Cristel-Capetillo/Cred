namespace Clothing {
    public class EventClothesChanged {
        public Wearable Wearable { get; }

        public EventClothesChanged(Wearable wearable) {
            Wearable = wearable;
        }
    }
}