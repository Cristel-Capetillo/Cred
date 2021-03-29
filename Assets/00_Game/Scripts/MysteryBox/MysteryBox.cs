using System;
using System.Collections;
using Clothing;
using Clothing.Inventory;
using UnityEngine;
using Utilities;

namespace MysteryBox {
    public class MysteryBox : MonoBehaviour {
        
        [SerializeField] float spawnRewardAfterDelay = 1.5f;
        [SerializeField] float destroyDelay = 2f;

        LootTable lootTable;

        void Start() {
            StartCoroutine(StartRewardProcess(spawnRewardAfterDelay));
        }

        public void LootTable(LootTable pLootTable) {
            this.lootTable = pLootTable;
        }

        IEnumerator StartRewardProcess(float delay) {
            yield return new WaitForSeconds(delay);
            var reward = lootTable.Reward();
            ShowReward(reward);
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(reward, 1));
            Destroy(gameObject, destroyDelay);
        }
        
        void ShowReward(CombinedWearables reward) {
            EventBroker.Instance().SendMessage(new EventMysteryBoxOpened(reward));
            EventBroker.Instance().SendMessage(new EventShowReward(reward));
        }
    }
}