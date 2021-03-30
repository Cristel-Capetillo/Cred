using System;
using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        public Transform instanceParent;
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
            var tmp = GetItemStats(nameOfCombinedWearable);
            return Convert.ToInt32(tmp[InventoryData.Amount]);
        }

        public bool CombinedWearableExists(string id) {
            if (combinedWearableDataToSave == null) {
                combinedWearableDataToSave = new Dictionary<string, object>();
                return false;
            }

            return combinedWearableDataToSave.ContainsKey(id);
        }

        Dictionary<string, object> GetItemStats(string itemId) {
            return (Dictionary<string, object>) combinedWearableDataToSave[itemId];
        }

        int UpdateAmount(string nameOfCombinedWearable, int amountToUpdate) {
            return Amount(nameOfCombinedWearable) + amountToUpdate;
            // var itemStatsDictionary = GetItemStats(nameOfCombinedWearable);
            // itemStatsDictionary[InventoryData.Amount] = sum;
            // combinedWearableDataToSave[nameOfCombinedWearable] = itemStatsDictionary;
        }

        void UpdatePlayerInventory(EventUpdatePlayerInventory wearableEvent) {
            var id = GetName(wearableEvent.combinedWearable);
            var wearable = wearableEvent.combinedWearable;

            print($"Style Points: {wearable.stylePoints}");
            if (!CombinedWearableExists(id)) {
                wearable = GenerateNewCombinedWearable(wearable);
                combinedWearableDataToSave.Add(id, inventoryData.StatList(wearable));
            }

            wearable.Amount = UpdateAmount(id, wearableEvent.addOrSubtractAmount);

            if (CombinedWearableAmountIsZero(id)) {
                if (combinedWearableDataToSave.ContainsKey(InventoryData.IsPredefined) && !(bool) combinedWearableDataToSave[InventoryData.IsPredefined]) {
                    combinedWearableDataToSave.Remove(id);
                }
            }

            wearable.ShouldBeInteractable();
            EventBroker.Instance().SendMessage(new EventSortInventory());
            saveHandler.Save(this);
        }

        CombinedWearables GenerateNewCombinedWearable(CombinedWearables wearable) {
            var instance = InstantiateCombinedWearables();
            instance.wearable = wearable.wearable;
            instance.rarity = wearable.rarity;
            instance.clothingType = wearable.clothingType;
            instance.stylePoints = wearable.stylePoints;
            return instance;
        }

        CombinedWearables InstantiateCombinedWearables() {
            return Instantiate(combinedWearablesTemplate, instanceParent);
        }

        bool CombinedWearableAmountIsZero(string combinedWearablesId) {
            return Amount(combinedWearablesId) < 1;
        }

        public Dictionary<string, object> ToBeSaved() {
            return combinedWearableDataToSave;
        }

        public void OnLoad(Dictionary<string, object> value) {
            //combinedWearableDataToSave = value;
            
            NoSaveFileFound();
            return;

            if (value == null) {
                NoSaveFileFound();
                return;
            }

            StartCoroutine(RestoreData(value));
        }

        void NoSaveFileFound() {
            EventBroker.Instance().SendMessage(new EventSpawnPredefinedWearables(true));
            EventBroker.Instance().SendMessage(new EventSortInventory());
        }

        IEnumerator RestoreData(Dictionary<string, object> value) {
            print($"[PlayerInventory_RestoreData]\nDictionary size: {value.Count}");
            var wearableList = new List<Wearable>();
            yield return Addressables.LoadAssetsAsync<Wearable>(inventoryData.wearablesAddress, wearable => { wearableList.Add(wearable); });

            foreach (var combinedWearable in value) {
                var combinedWearableInstance = InstantiateCombinedWearables();
                combinedWearableInstance.transform.localScale = Vector3.one;
                var combinedWearablesStatsDictionary = (Dictionary<string, object>) combinedWearable.Value;

                yield return AssignWearables(combinedWearablesStatsDictionary, combinedWearableInstance, wearableList);

                AssignCombinedWearableData(combinedWearableInstance, combinedWearablesStatsDictionary);
                combinedWearableInstance.ShouldBeInteractable();
            }

            yield return CallVariousEvents();
            EventBroker.Instance().SendMessage(new EventUpdateWearableHud());
        }

        IEnumerator CallVariousEvents() {
            yield return new WaitForSeconds(1f);
            EventBroker.Instance().SendMessage(new EventSortInventory());
            yield return null;
        }

        void AssignCombinedWearableData(CombinedWearables wearableInstance, Dictionary<string, object> wearablesStatsDictionary) {
            wearableInstance.Amount = Convert.ToInt32(wearablesStatsDictionary[InventoryData.Amount]);
            wearableInstance.stylePoints = Convert.ToInt32(wearablesStatsDictionary[InventoryData.StylePoints]);
            wearableInstance.rarity = inventoryData.allRarities[wearablesStatsDictionary[InventoryData.Rarity].ToString()];
            wearableInstance.clothingType = inventoryData.allClothingTypes[wearablesStatsDictionary[InventoryData.ClothingType].ToString()];
        }

        IEnumerator AssignWearables(Dictionary<string, object> combinedWearablesStatsDictionary, CombinedWearables combinedWearableInstance, List<Wearable> wearables) {
            var wearableCount = Convert.ToInt32(combinedWearablesStatsDictionary[InventoryData.WearableCount]);
            for (var sortIndex = 0; sortIndex < wearableCount; sortIndex++) {
                foreach (var s in wearables) {
                    if (combinedWearablesStatsDictionary.ContainsKey(s.ToString() + sortIndex)) {
                        combinedWearableInstance.wearable.Add(s);
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}