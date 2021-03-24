using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        [SerializeField] CombinedWearables combinedWearablesTemplate;

        public InventoryData inventoryData;
        SaveHandler saveHandler;
        public readonly Dictionary<CombinedWearables, int> combineWearablesAmount = new Dictionary<CombinedWearables, int>();
        Dictionary<string, object> combinedWearableDataToSave = new Dictionary<string, object>();

        void Start() {
            saveHandler = new SaveHandler("Inventory");
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
            inventoryData.Setup();
            saveHandler.Load(this);
        }


        //TODO unlocked Wearable?
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
        }

        void UpdatePlayerInventory(EventUpdatePlayerInventory wearableEvent) {
            var id = GetId(wearableEvent.combinedWearable);

            if (!CombineWearableExists(id)) {
                GenerateNewCombinedWearable(wearableEvent.combinedWearable);
                combineWearablesAmount.Add(wearableEvent.combinedWearable, 0);
            }

            combineWearablesAmount[wearableEvent.combinedWearable] += wearableEvent.addOrSubtractAmount;

            if (CombinedWearableAmountIsZero(wearableEvent.combinedWearable)) {
                combineWearablesAmount.Remove(wearableEvent.combinedWearable);
                combinedWearableDataToSave.Remove(id);
                return;
            }

            var wearableStatsList = WearableStatsDictionary(wearableEvent.combinedWearable);
            combinedWearableDataToSave[id] = wearableStatsList;
            saveHandler.Save(this);
        }

        bool CombineWearableExists(string id) {
            return combinedWearableDataToSave.ContainsKey(id);
        }

        void GenerateNewCombinedWearable(CombinedWearables wearableEvent) {
            var instance = InstantiateCombinedWearables();
            instance.wearable = wearableEvent.wearable;
            instance.rarity = wearableEvent.rarity;
            instance.clothingType = wearableEvent.clothingType;
        }

        CombinedWearables InstantiateCombinedWearables() {
            return Instantiate(combinedWearablesTemplate, transform);
        }

        bool CombinedWearableAmountIsZero(CombinedWearables combinedWearables) {
            return combineWearablesAmount[combinedWearables] < 1;
        }

        string GetId(CombinedWearables wearableEvent) {
            return wearableEvent.ToString();
        }

        Dictionary<string, object> WearableStatsDictionary(CombinedWearables combinedWearables) {
            var wearableStatsList = new Dictionary<string, object> {{InventoryData.WearableCount, combinedWearables.wearable.Count.ToString()}};

            var sortIndex = 0;
            foreach (var data in combinedWearables.wearable) {
                wearableStatsList.Add(data.ToString() + sortIndex, "");
                sortIndex++;
            }

            wearableStatsList.Add(InventoryData.Amount, combineWearablesAmount[combinedWearables].ToString());
            wearableStatsList.Add(InventoryData.Rarity, combinedWearables.rarity.name);
            wearableStatsList.Add(InventoryData.ClothingType, combinedWearables.clothingType.name);

            return wearableStatsList;
        }

        public Dictionary<string, object> ToBeSaved() {
            return combinedWearableDataToSave;
        }

        public void OnLoad(Dictionary<string, object> value) {
            combinedWearableDataToSave = value;

            if (value == null) return;

            foreach (var combinedWearable in value) {
                var combinedWearableInstance = InstantiateCombinedWearables();
                var combinedWearablesStatsDictionary = (Dictionary<string, object>) combinedWearable.Value;
                var wearableCount = Convert.ToInt32(combinedWearablesStatsDictionary[InventoryData.WearableCount]);

                for (var sortIndex = 0; sortIndex < wearableCount; sortIndex++) {
                    foreach (var s in inventoryData.wearables) {
                        if (combinedWearablesStatsDictionary.ContainsKey(s.ToString() + sortIndex)) {
                            combinedWearableInstance.wearable.Add(s);
                            break;
                        }
                    }
                }

                combinedWearableInstance.rarity = inventoryData.allRarities[combinedWearablesStatsDictionary[InventoryData.Rarity].ToString()];
                combinedWearableInstance.clothingType = inventoryData.allClothingTypes[combinedWearablesStatsDictionary[InventoryData.ClothingType].ToString()];
                combineWearablesAmount[combinedWearableInstance] = Convert.ToInt32(combinedWearablesStatsDictionary[InventoryData.Amount]);
            }
        }
    }
}