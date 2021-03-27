﻿using System;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class SortInventory : MonoBehaviour {
        public Transform instanceParent;

        public InventoryContents[] contents;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventOrganiseInventory>(CategoriesWearables);
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventOrganiseInventory>(CategoriesWearables);
        }

        void CategoriesWearables(EventOrganiseInventory sort) {
            foreach (var wearable in instanceParent.GetComponentsInChildren<CombinedWearables>()) {
                foreach (var content in contents) {
                    if (wearable.clothingType == content.clothingType) {
                        if (wearable.rarity.name == "Basic") {
                            wearable.transform.SetParent(content.basic);
                        }

                        if (wearable.rarity.name == "Normal") {
                            wearable.transform.SetParent(content.normal);
                        }

                        if (wearable.rarity.name == "Designer") {
                            wearable.transform.SetParent(content.design);
                        }
                    }
                }
            }
            //
            // combinedWearables.GetComponent<CombinedUI>().UpdateUI(combinedWearables);
            // combinedWearables.transform.localScale = Vector3.one;
        }
    }
}