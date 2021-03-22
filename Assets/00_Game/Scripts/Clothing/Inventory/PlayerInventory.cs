using System;
using System.Collections.Generic;
using ClientMissions.Data;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        Dictionary<string, object> newClothing = new Dictionary<string, object>();
        SaveHandler saveHandler;

        public Wearable wearable;

        void Start() {
            saveHandler = new SaveHandler("Inventory");
            EventBroker.Instance().SubscribeMessage<EventAddToInventory>(AddToInventory);
            EventBroker.Instance().SendMessage(new EventAddToInventory(wearable, 1));
            EventBroker.Instance().SendMessage(new EventAddToInventory(wearable, -1));
        }

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
            id += wearableEvent.wearable.ToString();

            var wearableStatsList = WearableStatsList(wearableEvent.wearable);

            if (!newClothing.ContainsKey(id)) {
                newClothing[id] = wearableStatsList;
            }
            wearableEvent.wearable.SetAmount(wearableEvent.addOrSubtractAmount);
            wearableStatsList.Add(wearableEvent.wearable.Amount.ToString());
        }

        List<object> WearableStatsList(Wearable wearable) {
            var wearableStatsList = new List<object>();
            foreach (var data in wearable.ColorData) {
                wearableStatsList.Add(data.GetHexColorID());
            }

            wearableStatsList.Add(wearable.Rarity.ToString());
            wearableStatsList.Add(wearable.ClothingType.ToString());
            wearableStatsList.Add(wearable.Texture.ToString());
            wearableStatsList.Add(wearable.Sprite.ToString());
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