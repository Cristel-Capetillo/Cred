using System;
using System.Collections;
using UnityEngine;

namespace MysteryBox {
    public class MysteryBox : MonoBehaviour {
        
        [SerializeField] Vector3 rewardSpawnOffset;
        [SerializeField] float spawnRewardAfterDelay = 1.5f;
        [SerializeField] float destroyDelay = 5f;
        
        LootTable lootTable;
        
        void Start() {
            StartCoroutine(StartRewardProcess(spawnRewardAfterDelay));
        }
        
        public void LootTable(LootTable pLootTable) {
            this.lootTable = pLootTable;
        }

        IEnumerator StartRewardProcess(float delay) {
            yield return new WaitForSeconds(delay);
            ShowReward();
            Destroy(gameObject, destroyDelay);
        }
        
        void ShowReward() {
            Instantiate(lootTable.Reward().gameObject, transform.position + rewardSpawnOffset, Quaternion.identity);
        }
        
        void OnDestroy() {
            StopCoroutine(nameof(StartRewardProcess));
        }
    }
}