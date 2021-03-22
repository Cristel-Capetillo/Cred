using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class PlayerInventory : MonoBehaviour, ISavable<Dictionary<string, object>> {
        Dictionary<string, object> newClothing = new Dictionary<string, object>();
        SaveHandler saveHandler;

        void Start() {
            saveHandler = new SaveHandler("Inventory");

            EventBroker.Instance().SubscribeMessage<EventAddToInventory>(AddToInventory);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToInventory>(AddToInventory);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.F5)) {
                saveHandler.Save(this);
            }

            if (Input.GetKeyDown(KeyCode.F9)) {
                saveHandler.Load(this);
            }
        }

        void GenerateNewClothingItem(Wearable wearable) {
            var id = "";
            var wearableStatsList = new List<object>();
            id += wearable.ToString();
            foreach (var data in wearable.ColorData) {
                wearableStatsList.Add(data.GetHexColorID());
            }


            wearableStatsList.Add(wearable.Rarity.ToString());
            wearableStatsList.Add(wearable.ClothingType.ToString());
            wearableStatsList.Add(wearable.Texture.ToString());
            wearableStatsList.Add(wearable.Sprite.ToString());

            newClothing[id] = wearableStatsList;

            wearable.SetAmount(1);

            wearableStatsList.Add(wearable.Amount.ToString());
        }

        void AddToInventory(EventAddToInventory inventory) {
            GenerateNewClothingItem(inventory.wearable);
        }

        public Dictionary<string, object> ToBeSaved() {
            return newClothing;
        }

        public void OnLoad(Dictionary<string, object> value) {
            newClothing = value;
        }
    }
}