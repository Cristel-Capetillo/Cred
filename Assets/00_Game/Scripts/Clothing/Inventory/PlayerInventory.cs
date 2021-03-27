using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<string> {
        public Transform[] contents;

        public CombinedWearables combinedWearablesTemplate;

        public InventoryData inventoryData;
        SaveHandler saveHandler;

        //value is of type Dictionary<string, object>
        Dictionary<string, Dictionary<object, CombinedWearables>> combinedWearableDataToSave = new Dictionary<string, Dictionary<object, CombinedWearables>>();


        Dictionary<string, Dictionary<object, TestClass>> test = new Dictionary<string, Dictionary<object, TestClass>>();

        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(UpdatePlayerInventory);
            saveHandler = new SaveHandler("Inventory");
            inventoryData.Setup();
            // foreach (var f in inventoryData.firstSave) {
            //     EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(f, 1));
            // }

            // test.Add("ID", new Dictionary<object, TestClass>());
            // test["ID"].Add("nothing", new TestClass());
            // test["ID"]["nothing"].Amount = 20;
            // var tmp = JsonConvert.SerializeObject(test, new JsonSerializerSettings() {
            //     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            // });

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

        public CombinedWearables GetItem(CombinedWearables combinedWearables) {
            return combinedWearableDataToSave[GetName(combinedWearables)][combinedWearables];
        }

        /// <summary>
        /// Returns the amount of given items owned as an integer.
        /// Use PlayerInventory.GetName to get the name
        /// </summary>
        void UpdateAmount(CombinedWearables combinedWearables, int amountToUpdate) {
            GetItem(combinedWearables).Amount += amountToUpdate;
        }

        public bool CombinedWearableExists(string id) {
            if (combinedWearableDataToSave == null) {
                combinedWearableDataToSave = new Dictionary<string, Dictionary<object, CombinedWearables>>();
                return false;
            }

            return combinedWearableDataToSave.ContainsKey(id);
        }

        void SpawnPredefined() {
            foreach (var combination in inventoryData.predefinedCombinations) {
                if (combinedWearableDataToSave.ContainsKey(GetName(combination))) continue;
                var instance = Instantiate(combination);
                CategoriesWearables(instance);
            }
        }


        void UpdatePlayerInventory(EventUpdatePlayerInventory wearableEvent) {
            var id = GetName(wearableEvent.combinedWearable);
            var wearable = wearableEvent.combinedWearable;

            if (!CombinedWearableExists(id)) {
                GenerateNewCombinedWearable(wearable);
                combinedWearableDataToSave[id] = new Dictionary<object, CombinedWearables> {[wearable] = wearable};
            }

            UpdateAmount(wearable, wearableEvent.addOrSubtractAmount);

            if (CombinedWearableAmountIsZero(wearable)) {
                if (!GetItem(wearable).isPredefined) {
                    combinedWearableDataToSave.Remove(id);
                }
            }

            test.Add(id, new Dictionary<object, TestClass>());
            test[id].Add(wearable.name, new TestClass(wearable.wearable));
            test[id][wearable.name].rarity = wearable.rarity;
            test[id][wearable.name].clothingType = wearable.clothingType;
            saveHandler.Save(this);
        }

        CombinedWearables GenerateNewCombinedWearable(CombinedWearables wearableEvent) {
            var instance = InstantiateCombinedWearables();
            instance.wearable = wearableEvent.wearable;
            instance.rarity = wearableEvent.rarity;
            instance.clothingType = wearableEvent.clothingType;
            return instance;
        }

        CombinedWearables InstantiateCombinedWearables() {
            return Instantiate(combinedWearablesTemplate, transform);
        }

        bool CombinedWearableAmountIsZero(CombinedWearables combinedWearablesId) {
            return GetItem(combinedWearablesId).Amount < 1;
        }

        public string ToBeSaved() {
            return JsonConvert.SerializeObject(test, new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public void OnLoad(string value) {
            if (string.IsNullOrEmpty(value)) {
                foreach (var f in inventoryData.firstSave) {
                    EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(f, 1));
                    // var go = GenerateNewCombinedWearable(f);
                    // UpdateAmount(GetItem(go), 1);
                    // CategoriesWearables(go);
                }

                SpawnPredefined();
                return;
            }

            test = JsonUtility.FromJson<Dictionary<string, Dictionary<object, TestClass>>>(value);
            //combinedWearableDataToSave = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<object, CombinedWearables>>>(value);


            print($"Dictionary size: {test.Count}");
            foreach (var wearable in test) {
                var wearableInstance = InstantiateCombinedWearables();
                AssignWearables(wearableInstance, wearable.Value[wearable]);

                AssignCombinedWearableData(wearableInstance, wearable.Value[wearable]);
                CategoriesWearables(wearableInstance);
            }

            SpawnPredefined();
        }

        void AssignCombinedWearableData(CombinedWearables wearableInstance, TestClass testClass) {
            wearableInstance.Amount = GetItem(wearableInstance).Amount;
            wearableInstance.stylePoints = wearableInstance.stylePoints;
            wearableInstance.rarity = inventoryData.allRarities[GetItem(wearableInstance).rarity];
            wearableInstance.clothingType = inventoryData.allClothingTypes[GetItem(wearableInstance).clothingType];
        }

        void AssignWearables(CombinedWearables wearableInstance, TestClass testClass) {
            var wearableCount = Convert.ToInt32(wearableInstance.wearable.Count);
            for (var sortIndex = 0; sortIndex < wearableCount; sortIndex++) {
                foreach (var s in inventoryData.wearables) {
                    if (test[GetName(wearableInstance)].ContainsKey(s.ToString())) {
                        wearableInstance.wearable.Add(s);
                        break;
                    }
                }
            }
        }

        void CategoriesWearables(CombinedWearables combinedWearables) {
            if (combinedWearables.clothingType.name == "Shirts") {
                combinedWearables.transform.SetParent(contents[0]);
            }

            if (combinedWearables.clothingType.name == "Pants") {
                combinedWearables.transform.SetParent(contents[1]);
            }

            if (combinedWearables.clothingType.name == "Jackets") {
                combinedWearables.transform.SetParent(contents[2]);
            }

            if (combinedWearables.clothingType.name == "Shoes") {
                combinedWearables.transform.SetParent(contents[3]);
            }

            if (combinedWearables.clothingType.name == "Accessories") {
                combinedWearables.transform.SetParent(contents[4]);
            }

            if (combinedWearables.clothingType.name == "Skirts") {
                combinedWearables.transform.SetParent(contents[5]);
            }

            combinedWearables.GetComponent<CombinedUI>().UpdateUI(combinedWearables);
            combinedWearables.transform.localScale = Vector3.one;
        }
    }
}