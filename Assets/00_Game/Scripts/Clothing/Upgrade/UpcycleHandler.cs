using System;
using UnityEngine;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleHandler : MonoBehaviour {
        WearableListMessage wearableListMessage;
        Wearable newUpcycledWearable;

        void Start() {
            EventBroker.Instance().SubscribeMessage<MessageUpCycleClothes>(UpCycleCombine);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<MessageUpCycleClothes>(UpCycleCombine);
        }

        void UpCycleCombine(MessageUpCycleClothes messageUpCycleClothes) {

            Wearable[] wearables = new Wearable[2] { messageUpCycleClothes.Wearable1, messageUpCycleClothes.Wearable2};

            foreach (var chosenWearable in wearables)
            {
                //TODO fix
                //chosenWearable.Amount -= 1;
                // chosenWearable.Value.SetAmount(chosenWearable.Value.Amount - 1);

            }
            EventBroker.Instance().SendMessage(new MessageUpCycleClothes(messageUpCycleClothes.Wearable1, messageUpCycleClothes.Wearable2));
        }

        void OnLoadWearableData() {
            throw new Exception("Not implemented yet!");
        }
    }
}