using System;
using System.Collections.Generic;
using Clothing.Inventory;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Dictionary<string, Rarity> combineWearablesDic = new Dictionary<string, Rarity>();

        public Image[] slots;

        public Button upCycleConfirmButton;

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().SubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().UnsubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.F6)) {
                foreach (var rarity in combineWearablesDic) {
                    print($"{rarity.Key}  {rarity.Value}");
                }
            }
        }

        public void AssignUpCycleSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignToSlot(eventAddUpCycleClothes.combinedWearable);
        }

        void AssignToSlot(CombinedWearables combinedWearables) {
            for (var i = 0; i < slots.Length; i++) {
                if (slots[i].transform.childCount > 0) {
                    if (!combineWearablesDic.ContainsValue(combinedWearables.rarity) || combineWearablesDic.ContainsKey(PlayerInventory.GetName(combinedWearables))) {
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
                    combineWearablesDic[PlayerInventory.GetName(combinedWearables)] = combinedWearables.rarity;
                    break;
                }
            }

            EventBroker.Instance().SendMessage(new EventValidateConfirmButton(slots[0].transform.childCount > 0 && slots[1].transform.childCount > 0));
        }

        void UpdateConfirmButton(EventValidateConfirmButton validateConfirmButton) {
            upCycleConfirmButton.interactable = validateConfirmButton.validateButton;
            if (validateConfirmButton.combinedWearables != null) {
                combineWearablesDic.Remove(PlayerInventory.GetName(validateConfirmButton.combinedWearables));
            }
        }

        public void OnConfirm() {
            var wearableInSlots = new List<CombinedWearables>();

            foreach (var slot in slots) {
                wearableInSlots.Add(slot.GetComponentInChildren<CombinedWearables>());
            }

            var instance = Instantiate(FindObjectOfType<PlayerInventory>().combinedWearablesTemplate);
            instance.rarity = wearableInSlots[0].rarity;
            instance.clothingType = wearableInSlots[0].clothingType;
            instance.isPredefined = false;

            AssignWearableSlots(wearableInSlots, instance);

            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearableInSlots[0], -1));
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearableInSlots[1], -1));
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(instance, 1));
            Destroy(instance.gameObject);
            foreach (var slot in slots) {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        static void AssignWearableSlots(List<CombinedWearables> wearableInSlots, CombinedWearables instance) {
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
        }
    }
}