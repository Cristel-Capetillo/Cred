using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing {
    public class InventoryDataHandler : MonoBehaviour {
        public readonly Dictionary<ClothingType, List<Wearable>> wearableDictionary = new Dictionary<ClothingType, List<Wearable>>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(WearableListMessage wearableListMessage) {
            var wearables = wearableListMessage.Wearables;
            if (!wearableDictionary.ContainsKey(wearables[0].ClothingType)) {
                wearableDictionary.Add(wearables[0].ClothingType, wearables);
            }
            else {
                wearableDictionary[wearables[0].ClothingType].AddRange(wearables);
            }
            wearableDictionary[wearables[0].ClothingType] = wearableDictionary[wearables[0].ClothingType]
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