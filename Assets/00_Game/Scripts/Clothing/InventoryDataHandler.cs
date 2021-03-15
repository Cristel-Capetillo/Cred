using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing {
    public class InventoryDataHandler : MonoBehaviour {
        [SerializeField] List<Rarity> raritySortOrder = new List<Rarity>();
        public Dictionary<ClothingType, List<Wearable>> WearableDictionary = new Dictionary<ClothingType, List<Wearable>>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(WearableListMessage wearableListMessage) {
            var temp = wearableListMessage.Wearables;
            if (!WearableDictionary.ContainsKey(temp[0].ClothingType)) {
                WearableDictionary.Add(temp[0].ClothingType, temp);
            }
            else {
                WearableDictionary[temp[0].ClothingType].AddRange(temp);
            }
            Debug.Log(WearableDictionary.Count + " WearableDictonaryCount");
            WearableDictionary[temp[0].ClothingType] = raritySortOrder.OrderBy(w => w.name).ToList();

        }
    }

    public class WearableListMessage {
        public List<Wearable> Wearables;

        public WearableListMessage(List<Wearable> wearables) {
            Wearables = wearables;
        }
    }
}