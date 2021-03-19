using UnityEngine;

namespace MysteryBox {
    public class MysteryBox : MonoBehaviour {
        
        LootTable lootTable;
        [SerializeField] float destroyDelay = 5f;

        void Start() {
            Destroy(gameObject, destroyDelay);
        }
        
        public void AssignLootTable(LootTable lootTable) {
            this.lootTable = lootTable;
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