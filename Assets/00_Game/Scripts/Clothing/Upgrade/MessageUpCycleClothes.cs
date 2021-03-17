namespace Clothing.Upgrade {
    public class MessageUpCycleClothes {
        public Wearable Wearable1 { get;}
        public Wearable Wearable2 { get;}


        public MessageUpCycleClothes(Wearable firstwearable, Wearable secondwearable) {
            Wearable1 = firstwearable;
            Wearable2 = secondwearable;
        }
    }

    public class MessageDonateClothes {
        public Wearable Wearable { get;}

        public MessageDonateClothes(Wearable wearable) {
            Wearable = wearable;
        }
    }
}