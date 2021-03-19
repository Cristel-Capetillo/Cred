using System.Collections;
using UnityEngine;
using Utilities;

namespace MysteryBox {
    public class MysteryBox : MonoBehaviour {
        Animator animator;
        LootTable lootTable;
        bool opened;

        void Start() {
            animator = gameObject.GetComponent<Animator>();
        }
        
        public void StartAnimation() {
            animator.SetTrigger("OpenLootBox");
        }

        IEnumerator temp() {
            opened = true;
            StartAnimation();
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
            EventBroker.Instance().SendMessage(new EventMysteryBoxOpened(lootTable.Reward()));
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 6)) {
                    if (hit.collider != null) {
                        StartAnimation();
                    }
                }
            }
        }
    }
}
