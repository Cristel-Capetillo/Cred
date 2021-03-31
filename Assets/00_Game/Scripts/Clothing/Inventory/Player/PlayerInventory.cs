using System;
using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        public Transform instanceParent;
        public CombinedWearables combinedWearablesTemplate;
        public InventoryData inventoryData;
        SaveHandler saveHandler;

        //value is of type Dictionary<string, object>
        Dictionary<string, object> combinedWearableDataToSave = new Dictionary<string, object>();
        Dictionary<string, Dictionary<string, object>> temporaryData = new Dictionary<string, Dictionary<string, object>>();

        PredefinedAssets predefinedAssets;

        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
            predefinedAssets = GetComponent<PredefinedAssets>();
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

        public bool CombinedWearableExists(string id) {
            if (combinedWearableDataToSave == null) {
                combinedWearableDataToSave = new Dictionary<string, object>();
                return false;
            }

            return combinedWearableDataToSave.ContainsKey(id);
        }

        void UpdatePlayerInventory(EventUpdatePlayerInventory wearableEvent) {
            var id = GetName(wearableEvent.combinedWearable);
            var wearable = wearableEvent.combinedWearable;


            if (!CombinedWearableExists(id)) {
                wearable = GenerateNewCombinedWearable(wearable);
                combinedWearableDataToSave.Add(id, inventoryData.StatList(wearable));
                temporaryData.Add(id, inventoryData.StatList(wearable));
            }

            wearable.Amount += wearableEvent.addOrSubtractAmount;
            EventBroker.Instance().SendMessage(new EventUpdateAmount(id, wearable.Amount));
            if (CombinedWearableAmountIsZero(wearable)) {
                if (!ValidatePredefined(id)) {
                    combinedWearableDataToSave.Remove(id);
                    temporaryData.Remove(id);
                    UpdateHud(wearable);
                    saveHandler.Save(this);
                    EventBroker.Instance().SendMessage(new EventDestroyCombinedWearable(id));
                    return;
                }
            }

            wearable.ShouldBeInteractable();

            AssignNewValues(id, wearable);


            UpdateHud(wearable);

            combinedWearableDataToSave[id] = temporaryData[id];
            EventBroker.Instance().SendMessage(new EventSortInventory());
            saveHandler.Save(this);
        }

        bool ValidatePredefined(string id) {
            return predefinedAssets.predefinedDictionary.ContainsKey(id);
        }

        void AssignNewValues(string id, CombinedWearables wearable) {
            temporaryData[id][InventoryData.Amount] = wearable.Amount;
            temporaryData[id][InventoryData.StylePoints] = wearable.stylePoints;
            temporaryData[id][InventoryData.IsPredefined] = wearable.isPredefined;
        }

        static void UpdateHud(CombinedWearables wearable) {
            wearable.GetComponent<IconUpdate>().UpdateImages();
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

        bool CombinedWearableAmountIsZero(CombinedWearables wearable) {
            return wearable.Amount < 1;
        }

        public Dictionary<string, object> ToBeSaved() {
            return combinedWearableDataToSave;
        }

        public void OnLoad(Dictionary<string, object> value) {
            combinedWearableDataToSave = value;

            if (value == null) {
                NoSaveFileFound();
                return;
            }

            StartCoroutine(RestoreData(value));
        }

        void NoSaveFileFound() {
            EventBroker.Instance().SendMessage(new EventSpawnPredefinedWearables(combinedWearableDataToSave, true));
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
                temporaryData[combinedWearableInstance.ToString()] = combinedWearablesStatsDictionary;
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