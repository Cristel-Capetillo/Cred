using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Clothing.Inventory {
    public class InventoryDataHandler : MonoBehaviour {
        public readonly Dictionary<ClothingType, List<CombinedWearables>> wearableDictionary = new Dictionary<ClothingType, List<CombinedWearables>>();


        void Start() {
            EventBroker.Instance().SubscribeMessage<EventCombinedWearable>(OnLoadWearablesAssets);
        }


        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventCombinedWearable>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(EventCombinedWearable eventCombinedWearable) {
            var combinedWearableList = eventCombinedWearable.wearables;
            if (!wearableDictionary.ContainsKey(combinedWearableList[0].clothingType)) {
                wearableDictionary.Add(combinedWearableList[0].clothingType, combinedWearableList);
            }
            else {
                wearableDictionary[combinedWearableList[0].clothingType].AddRange(combinedWearableList);
            }

            wearableDictionary[combinedWearableList[0].clothingType] = wearableDictionary[combinedWearableList[0].clothingType]
                .OrderBy(combineWearable => combineWearable.rarity.Value).ToList();
        }
    }

    public class EventCombinedWearable {
        public readonly List<CombinedWearables> wearables;

        public EventCombinedWearable(List<CombinedWearables> wearables) {
            this.wearables = wearables;
        }
    }
}