using System;
using System.Collections.Generic;
using ClientMissions.Data;
using SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        Dictionary<string, object> newClothing = new Dictionary<string, object>();

        Dictionary<CombinedWearables, int> combineWearablesAmount = new Dictionary<CombinedWearables, int>();
        
        SaveHandler saveHandler;

        [FormerlySerializedAs("combineWearable")] public Wearable wearable;

        ScriptableObject combinedWearable;
        
        void Start() {
            saveHandler = new SaveHandler("Inventory");
            EventBroker.Instance().SubscribeMessage<EventAddToInventory>(AddToInventory);
            
        }

        //TODO unlocked Wearable?
        void OnDestroy() {
            
            EventBroker.Instance().UnsubscribeMessage<EventAddToInventory>(AddToInventory);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.F5)) {
                saveHandler.Save(this);
                print("saved");
            }

            if (Input.GetKeyDown(KeyCode.F9)) {
                saveHandler.Load(this);
            }
        }

        void AddToInventory(EventAddToInventory inventory) {
            GenerateNewClothingItem(inventory);
        }

        void GenerateNewClothingItem(EventAddToInventory wearableEvent) {
            var id = "";
            id += wearableEvent.combinedWearable.ToString();

            var wearableStatsList = WearableStatsList(wearableEvent.combinedWearable);

            if (!newClothing.ContainsKey(id)) {
                newClothing[id] = wearableStatsList;
            }

            // wearableEvent.wearable.SetAmount(wearableEvent.addOrSubtractAmount);
            // wearableStatsList.Add(wearableEvent.wearable.Amount.ToString());
        }

        List<object> WearableStatsList(CombinedWearables combinedWearables) {
            var wearableStatsList = new List<object>();
            foreach (var data in combinedWearables.wearable) {
                wearableStatsList.Add(data.colorData.GetHexColorID());
                wearableStatsList.Add(data.Texture.ToString());
                wearableStatsList.Add(data.Sprite.ToString());
            }

            //wearableStatsList.Add(wearable.Rarity.ToString());
            //wearableStatsList.Add(wearable.ClothingType.ToString());
            
            return wearableStatsList;
        }

        public Dictionary<string, object> ToBeSaved() {
            return newClothing;
        }

        public void OnLoad(Dictionary<string, object> value) {
            newClothing = value;
            
            
            
        }
    }
}