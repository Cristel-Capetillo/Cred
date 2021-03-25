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

        //public CombinedWearables newCombinedWearables;

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().SubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().UnsubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
        }

        public void AssignUpCycleSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignToSlot(eventAddUpCycleClothes.combinedWearable);
        }

        void AssignToSlot(CombinedWearables combinedWearables) {
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
                    Destroy(instance.GetComponent<AssignCombinedWearableToUpCycle>());
                    Destroy(instance.GetComponent<Button>());
                    combineWearablesDic[combinedWearables] = combinedWearables.rarity;
                    break;
                }
            }

            EventBroker.Instance().SendMessage(new EventValidateConfirmButton(slots[0].transform.childCount > 0 && slots[1].transform.childCount > 0));
        }

        void UpdateConfirmButton(EventValidateConfirmButton validateConfirmButton) {
            upCycleConfirmButton.interactable = validateConfirmButton.validateButton;
        }
        public void OnConfirm() {
            //EventBroker.Instance().SendMessage(new MessageUpCycleClothes(combineWearablesDic[slot1], combineWearablesDic[slot2]));
            var wearableInSlots = new List<CombinedWearables>();

            foreach (var slot in slots) {
                wearableInSlots.Add(slot.GetComponentInChildren<CombinedWearables>());
            }

            var instance = Instantiate(FindObjectOfType<PlayerInventory>().combinedWearablesTemplate);
            instance.rarity = wearableInSlots[0].rarity;
            instance.clothingType = wearableInSlots[0].clothingType;

            var count = wearableInSlots[0].wearable.Count;

            if (count < 3) {
                instance.wearable.Add(wearableInSlots[0].wearable[0]);
                instance.wearable.Add(wearableInSlots[1].wearable[1]);
            }
            else {
                instance.wearable.Add(wearableInSlots[0].wearable[0]);
                instance.wearable.Add(wearableInSlots[1].wearable[1]);
                instance.wearable.Add(wearableInSlots[1].wearable[2]);
            }

            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(instance, 1));
            Destroy(instance.gameObject);
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