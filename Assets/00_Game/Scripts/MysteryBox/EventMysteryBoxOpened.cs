using Clothing;

namespace MysteryBox {
    public class EventMysteryBoxOpened {
        public Wearable Reward;

        public EventMysteryBoxOpened(Wearable reward) {
            this.Reward = reward;
        }
    }
}