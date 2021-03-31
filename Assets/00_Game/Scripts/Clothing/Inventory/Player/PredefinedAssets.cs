using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PredefinedAssets : MonoBehaviour{
        public List<CombinedWearables> predefinedCombinations;

        public readonly Dictionary<string, CombinedWearables> predefinedDictionary = new Dictionary<string, CombinedWearables>();
        PlayerInventory playerInventory;
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSpawnPredefinedWearables>(SpawnPredefined);
            playerInventory = GetComponent<PlayerInventory>();
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
            foreach (var combination in predefinedCombinations){
                playerInventory.UpdatePlayerInventory2(combination, combination.rarity.name == "Basic" ? 1 : 0);
            }
            print("Done"); 
        }
    }
}