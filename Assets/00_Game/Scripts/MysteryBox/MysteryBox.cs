using System;
using System.Collections;
using UnityEngine;

namespace MysteryBox {
    public class MysteryBox : MonoBehaviour {
        
        [SerializeField] float spawnRewardAfterDelay = 1.5f;
        [SerializeField] float destroyDelay = 5f;

        LootTable lootTable;
        
        void Start() {
            StartCoroutine(StartRewardProcess(spawnRewardAfterDelay));
        }
        
        public void AssignLootTable(LootTable lootTable) {
            this.lootTable = lootTable;
        }

        IEnumerator StartRewardProcess(float delay) {
            yield return new WaitForSeconds(delay);
            //ShowReward();
            Destroy(gameObject, destroyDelay);
        }
        
        
        void ShowReward() {
            //play animation and spawn visual Representation of loot acquired, perhaps play sound
            Instantiate(lootTable.Reward()); 
        }
        void OnDestroy() {
            StopCoroutine(nameof(StartRewardProcess));
        }

        
        
        // public void StartAnimation() {
        //     animator.SetTrigger("OpenLootBox");
        // }
        //
        // IEnumerator temp() {
        //     opened = true;
        //     StartAnimation();
        //     yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        //     EventBroker.Instance().SendMessage(new EventMysteryBoxOpened(lootTable.Reward()));
        //     Debug.Log("Loot Box Opened");
        //     Destroy(gameObject);
        // }
        //
        // void Update() {
        //     if (Input.GetMouseButtonDown(0)) {
        //         RaycastHit hit;
        //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //         if (Physics.Raycast(ray, out hit, Mathf.Infinity, 6)) {
        //             if (hit.collider != null) {
        //                 Destroy(gameObject);
        //             }
        //         }
        //     }
        // }
    }
}