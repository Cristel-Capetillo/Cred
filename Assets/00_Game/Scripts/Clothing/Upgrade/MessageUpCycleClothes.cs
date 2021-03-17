
using UnityEngine;
namespace Clothing
{
    public class MessageUpCycleClothes
    {
        public Wearable Wearable1 { get; private set; }
        public Wearable Wearable2 { get; private set; }

   
        public MessageUpCycleClothes(Wearable firstwearable, Wearable secondwearable) {
           
            Wearable1 = firstwearable;
            Wearable2 = secondwearable;

        }
    }

    public class MessageDonateClothes
    {
        public Wearable Wearable { get; private set; }

        public MessageDonateClothes(Wearable wearable) { Wearable = wearable; }
    }
}
