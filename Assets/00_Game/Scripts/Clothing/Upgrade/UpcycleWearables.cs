using System;
using System.Collections.Generic;
using Clothing.Inventory;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Dictionary<CombinedWearables, Rarity> combineWearablesDic = new Dictionary<CombinedWearables, Rarity>();

        public Image[] slots;

        public Button upCycleConfirmButton;

        CombinedWearables newCombinedWearables;

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
        }

        public void AssignUpCycleSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            upCycleConfirmButton.interactable = AssignToSlot(eventAddUpCycleClothes.combinedWearable);
        }

        bool AssignToSlot(CombinedWearables combinedWearables) {
            for (var i = 0; i < slots.Length; i++) {
                if (slots[i].transform.childCount > 0) {
                    if (!combineWearablesDic.ContainsValue(combinedWearables.rarity) || combineWearablesDic.ContainsKey(combinedWearables)) {
                        break;
                    }
                }

                if (slots[i].transform.childCount < 1) {
                    var instance = Instantiate(combinedWearables, slots[i].transform, true);
                    var scale = combinedWearables.GetComponent<RectTransform>().localScale;
                    instance.transform.localPosition = Vector2.zero;
                    instance.GetComponent<RectTransform>().localScale = scale;
                    combineWearablesDic[combinedWearables] = combinedWearables.rarity;
                    break;
                }
            }

            return slots[0].transform.childCount > 0 && slots[1].transform.childCount > 0;
        }

        public void OnConfirm() {
            //EventBroker.Instance().SendMessage(new MessageUpCycleClothes(combineWearablesDic[slot1], combineWearablesDic[slot2]));
            var wearableInSlots = new List<CombinedWearables>();
            
            foreach (var slot in slots) {
                wearableInSlots.Add(slot.GetComponentInChildren<CombinedWearables>());
            }
            
            newCombinedWearables = gameObject.AddComponent<CombinedWearables>();
            newCombinedWearables.rarity = wearableInSlots[0].rarity;
             newCombinedWearables.clothingType = wearableInSlots[0].clothingType;


            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(newCombinedWearables, 1));
        }

        // public void CleanUpOnExitAndConfirm() {
        //     popupWindowUpCycle.SetActive(false);
        //     //popupWindowUpCycleDonate.ResetBools();
        //     slot1.sprite = null;
        //     slot1.GetComponentInChildren<Text>().text = null;
        //     slot2.sprite = null;
        //     slot2.GetComponentInChildren<Text>().text = null;
        //     buttonScriptList.Clear();
        //     // wearables[slot1].SetAmount(1);
        //     combineWearablesDic[slot1] = null;
        //     //wearables[slot2].SetAmount(1);
        //     combineWearablesDic[slot2] = null;
        // }

        // public void CleanUpOnWearableSelect() {
        //     EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
        //     EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = null;
        //     upcycleConfirmButton.interactable = false;
        //     if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>() == slot1) {
        //         // wearables[slot1].SetAmount(1);
        //         combineWearablesDic[slot1] = null;
        //     }
        //     else {
        //         //  wearables[slot2].SetAmount(1);
        //         combineWearablesDic[slot2] = null;
        //     }
        // }
    }
}