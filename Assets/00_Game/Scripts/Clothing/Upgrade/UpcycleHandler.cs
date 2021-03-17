using System;
using UnityEngine;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleHandler : MonoBehaviour {
        void Start() {
            EventBroker.Instance().SubscribeMessage<MessageUpCycleClothes>(OnLoadWearablesAssets);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<MessageUpCycleClothes>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(MessageUpCycleClothes messageUpCycleClothes) {
            Wearable item1 = messageUpCycleClothes.Wearable1;
            Wearable item2 = messageUpCycleClothes.Wearable2;
        }

        void OnLoadWearableData() {
            throw new Exception("Not implemented yet!");
        }
    }
}