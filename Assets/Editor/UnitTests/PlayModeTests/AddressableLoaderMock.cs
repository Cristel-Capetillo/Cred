using System.Collections.Generic;
using Clothing;
using UnityEngine;
using Utilities;

namespace Editor.UnitTests.PlayModeTests{
    public class AddressableLoaderMock : MonoBehaviour
    {
        public List<Wearable> wearablePants = new List<Wearable>();
        public List<Wearable> wearableShirts = new List<Wearable>();
        public Rarity common, rare, epic;
        public ClothingType pants, shirt;
        public void SendWearablePantsMessage(){
            EventBroker.Instance().SendMessage(new WearableListMessage(wearablePants));
        }
        public void SendWearableShirtsMessage(){
            EventBroker.Instance().SendMessage(new WearableListMessage(wearableShirts));
        }
    }
}
