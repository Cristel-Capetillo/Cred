using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        public CombinedWearables combinedWearablesTemplate;

        public InventoryData inventoryData;
        SaveHandler saveHandler;

        //value is of type Dictionary<string, object>
        Dictionary<string, object> combinedWearableDataToSave = new Dictionary<string, object>();


        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
            saveHandler = new SaveHandler("Inventory");
            inventoryData.Setup();
            saveHandler.Load(this);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.F9)) {
                saveHandler.Load(this);
            }
        }

        //TODO unlocked Wearable?
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
        }

        public static string GetName(CombinedWearables combinedWearable) {
            return combinedWearable.ToString();
        }

        /// <summary>
        /// Returns the amount of given items owned as an integer.
        /// Use PlayerInventory.GetName to get the name
        /// </summary>
        public int Amount(string nameOfCombinedWearable) {
            var tmp = ItemStats(nameOfCombinedWearable);
            return Convert.ToInt32(tmp[InventoryData.Amount]);
        }

        public bool CombinedWearableExists(string id) {
            if (combinedWearableDataToSave == null) {
                combinedWearableDataToSave = new Dictionary<string, object>();
                return false;
            }

            return combinedWearableDataToSave.ContainsKey(id);
        }

        Dictionary<string, object> ItemStats(string itemId) {
            var tmp = new Dictionary<string, object>();
            tmp = (Dictionary<string, object>) combinedWearableDataToSave[itemId];
            return tmp;
        }

        void UpdateAmount(string nameOfCombinedWearable, int amountToUpdate) {
            var sum = Amount(nameOfCombinedWearable) + amountToUpdate;
            var itemStatsDictionary = ItemStats(nameOfCombinedWearable);
            itemStatsDictionary[InventoryData.Amount] = sum;
            combinedWearableDataToSave[nameOfCombinedWearable] = itemStatsDictionary;
        }

        void UpdatePlayerInventory(EventUpdatePlayerInventory wearableEvent) {
            var id = GetName(wearableEvent.combinedWearable);
            if (!CombinedWearableExists(id)) {
                GenerateNewCombinedWearable(wearableEvent.combinedWearable);
                combinedWearableDataToSave.Add(id, inventoryData.StatList(wearableEvent.combinedWearable));
            }

            UpdateAmount(id, wearableEvent.addOrSubtractAmount);

            if (CombinedWearableAmountIsZero(id)) {
                combinedWearableDataToSave.Remove(id);
                return;
            }

            saveHandler.Save(this);
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

        bool CombinedWearableAmountIsZero(string combinedWearablesId) {
            return Amount(combinedWearablesId) < 1;
        }

        public Dictionary<string, object> ToBeSaved() {
            return combinedWearableDataToSave;
        }

        public void OnLoad(Dictionary<string, object> value) {
            combinedWearableDataToSave = value;

            if (value == null) {
                foreach (var f in inventoryData.firstSave) {
                    EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(f, 1));
                }

                return;
            }

            print($"Dictionary size: {value.Count}");
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

                combinedWearableInstance.stylePoints = Convert.ToInt32(combinedWearablesStatsDictionary[InventoryData.StylePoints]);
                combinedWearableInstance.rarity = inventoryData.allRarities[combinedWearablesStatsDictionary[InventoryData.Rarity].ToString()];
                combinedWearableInstance.clothingType = inventoryData.allClothingTypes[combinedWearablesStatsDictionary[InventoryData.ClothingType].ToString()];
            }
        }

        void AssignWearable(Dictionary<string, object> combinedWearablesStatsDictionary, CombinedWearables combinedWearableInstance) {
            
        }
    }
}