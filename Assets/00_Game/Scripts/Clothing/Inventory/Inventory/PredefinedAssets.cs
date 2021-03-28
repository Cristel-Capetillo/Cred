﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PredefinedAssets : MonoBehaviour {
        public List<CombinedWearables> predefinedCombinations;


        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSpawnPredefinedWearables>(SpawnPredefined);
        }

        void SpawnPredefined(EventSpawnPredefinedWearables defined) {
            foreach (var combination in predefinedCombinations) {
                if (defined.isFirstSave) {
                    FirstSave();
                    break;
                }

                if (defined.wearables.ContainsKey(PlayerInventory.GetName(combination))) continue;
                var instance = Instantiate(combination);
                EventBroker.Instance().SendMessage(new EventSortInventory());
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