using System;
using MysteryBox;
using UnityEngine;
using UnityEngine.UI;

namespace HUD.MysteryBox {
    public class MysteryBoxButton : MonoBehaviour {

        Button button;
        LootTable lootTable;
        [SerializeField] GameObject MysteryBoxPrefab;
        
        public void SpawnMysteryBox() {
            var instance =Instantiate(MysteryBoxPrefab);
            instance.GetComponent<global::MysteryBox.MysteryBox>().LootTable(lootTable);
        }
        
        public void AssignLootTable(LootTable lootTable) {
            this.lootTable = lootTable;
        }
    }
}