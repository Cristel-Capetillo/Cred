using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PredefinedAssets : MonoBehaviour {
        public List<CombinedWearables> predefinedCombinations;

        public readonly Dictionary<string, CombinedWearables> predefinedDictionary = new Dictionary<string, CombinedWearables>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSpawnPredefinedWearables>(SpawnPredefined);
            foreach (var combination in predefinedCombinations) {
                predefinedDictionary[combination.ToString()] = combination;
            }
        }

        void SpawnPredefined(EventSpawnPredefinedWearables defined) {
            foreach (var combination in predefinedDictionary) {
                if (defined.isFirstSave) {
                    FirstSave();
                    break;
                }

                if (defined.inventory.ContainsKey(combination.Key)) continue;
                Instantiate(combination.Value);
            }
        }

        void FirstSave() {
            foreach (var combination in predefinedCombinations) {
                if (combination.rarity.name == "Basic") {
                    EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combination, 1));
                }
                else {
                    EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combination, 0));
                }
            }
        }
    }
}