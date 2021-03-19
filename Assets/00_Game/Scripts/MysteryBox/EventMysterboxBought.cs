using Clothing;

namespace MysteryBox {
    public class EventMysteryboxBought {
        public readonly int cost;
        public EventMysteryboxBought(int cost) {
            this.cost = cost;
        }
    }

    public class EventMysteryBoxOpened {
        public Wearable reward;

        public EventMysteryBoxOpened(Wearable reward) {
            this.reward = reward;
        }
    }
}
