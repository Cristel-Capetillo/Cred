using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing {
    public class InventoryDataHandler : MonoBehaviour {
        public Dictionary<ClothingType, List<Wearable>> wearableDictionary = new Dictionary<ClothingType, List<Wearable>>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(WearableListMessage wearableListMessage) {
            var temp = wearableListMessage.Wearables;
            if (!wearableDictionary.ContainsKey(temp[0].ClothingType)) {
                wearableDictionary.Add(temp[0].ClothingType, temp);
            }
            else {
                wearableDictionary[temp[0].ClothingType].AddRange(temp);
            }
            wearableDictionary[temp[0].ClothingType] = wearableDictionary[temp[0].ClothingType]
                .OrderBy(rarity => rarity.Rarity.Value).ToList();
        }
    }

    public class WearableListMessage {
        public List<Wearable> Wearables;

        public WearableListMessage(List<Wearable> wearables) {
            Wearables = wearables;
        }
    }
}