using System;
using MysteryBox;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.MysteryBox {
    public class SelectionButton : MonoBehaviour {

        Button button;
        LootTable lootTable;
        [SerializeField] GameObject MysteryBoxPrefab;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventMysteryBoxBought>(ShouldBeActive);
        }

        void ShouldBeActive(EventMysteryBoxBought eventMysteryBoxBought) {
            button.interactable = FindObjectOfType<MysteryBoxInventory>().Owned > 0;
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