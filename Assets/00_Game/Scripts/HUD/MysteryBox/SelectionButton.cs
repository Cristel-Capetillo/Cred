using System;
using MysteryBox;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.MysteryBox {
    public class SelectionButton : MonoBehaviour {

        Button button;
        LootTable lootTable;
        MysteryBoxInventory mysteryBoxInventory;
        [SerializeField] GameObject MysteryBoxPrefab;

        void Start() {
            button = GetComponent<Button>();
            mysteryBoxInventory = FindObjectOfType<MysteryBoxInventory>();
            button.interactable = mysteryBoxInventory.Owned > 0;
            EventBroker.Instance().SubscribeMessage<EventMysteryBoxBought>(ShouldBeActive);
            
            Debug.Log("Selection Menu Spawned, Mystery Boxes Owned : " +mysteryBoxInventory.Owned);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventMysteryBoxBought>(ShouldBeActive);
        }

        void ShouldBeActive(EventMysteryBoxBought eventMysteryBoxBought) {
            button.interactable = mysteryBoxInventory.Owned > 0;
        }

        public void SpawnMysteryBox() {
            var instance =Instantiate(MysteryBoxPrefab);
            instance.GetComponent<global::MysteryBox.MysteryBox>().LootTable(lootTable);
        }
        
        public void AssignLootTable(LootTable lootTable) {
            this.lootTable = lootTable;
        }
    }
}