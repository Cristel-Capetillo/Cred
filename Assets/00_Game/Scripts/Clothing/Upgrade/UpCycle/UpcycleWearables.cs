using System.Collections.Generic;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.UpCycle {
    public class UpcycleWearables : MonoBehaviour {
        readonly Dictionary<string, Rarity> combineWearablesDic = new Dictionary<string, Rarity>();

        public Image[] slots;

        public Button upCycleConfirmButton;

        List<CombinedWearables> wearableInSlots = new List<CombinedWearables>();
        CanvasGroup canvasGroup;


        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().SubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
            EventBroker.Instance().SubscribeMessage<EventHideUpdateWindows>(ResetWindow);
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
            EventBroker.Instance().UnsubscribeMessage<EventValidateConfirmButton>(UpdateConfirmButton);
            EventBroker.Instance().UnsubscribeMessage<EventHideUpdateWindows>(ResetWindow);
        }


        void ResetWindow(EventHideUpdateWindows window) {
            if (window.shouldHide) {
                DeactivateWindow();
            }
        }

        void AssignUpCycleSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignToSlot(eventAddUpCycleClothes.combinedWearable);
        }

        void AssignToSlot(CombinedWearables combinedWearables) {
            if (combinedWearables.Amount <= 0) return;

            for (var i = 0; i < slots.Length; i++) {
                if (slots[i].transform.childCount > 0) {
                    if (!combineWearablesDic.ContainsValue(combinedWearables.rarity) || combineWearablesDic.ContainsKey(PlayerInventory.GetName(combinedWearables))) {
                        break;
                    }
                }

                if (slots[i].transform.childCount < 1) {
                    var instance = Instantiate(combinedWearables, slots[i].transform, true);
                    instance.Amount = combinedWearables.Amount;
                    instance.stylePoints = combinedWearables.stylePoints;
                    var scale = combinedWearables.GetComponent<RectTransform>().localScale;
                    instance.transform.localPosition = Vector2.zero;
                    instance.GetComponent<RectTransform>().localScale = scale;

                    instance.GetComponent<IconUpdate>().UpdateInformation();
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
            GenerateNewItem();
            foreach (var slot in slots) {
                Destroy(slot.transform.GetChild(0).gameObject);
            }


            DeactivateWindow();
        }

        void GenerateNewItem() {
            foreach (var slot in slots) {
                wearableInSlots.Add(slot.GetComponentInChildren<CombinedWearables>());
            }

            var instance = Instantiate(FindObjectOfType<PlayerInventory>().combinedWearablesTemplate);
            instance.rarity = wearableInSlots[0].rarity;
            instance.clothingType = wearableInSlots[0].clothingType;
            instance.stylePoints = wearableInSlots[0].stylePoints;
            instance.Amount = wearableInSlots[0].Amount;
            instance.isPredefined = false;
            instance.GetComponent<IconUpdate>().UpdateImages();

            AssignWearableSlots(wearableInSlots, instance);
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearableInSlots[0], -1));
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearableInSlots[1], -1));
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(instance, 1));
            EventBroker.Instance().SendMessage(new EventShowReward(instance));

            RecordAnalytics(instance);

            Destroy(instance.gameObject);
        }

        void RecordAnalytics(CombinedWearables instance) {
            UnityEngine.Analytics.Analytics.CustomEvent(
                "Confirm up cycle",
                new Dictionary<string, object> {
                    {"Confirm", instance.clothingType.name}
                });
        }

        public void CloseWindow() {
            DeactivateWindow();
        }

        void DeactivateWindow() {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;

            wearableInSlots.Clear();
            combineWearablesDic.Clear();
            EventBroker.Instance().SendMessage(new EventTogglePopWindow(false));
            Debug.Log(gameObject.name);
            if (slots[0].transform.childCount > 0) {
                Destroy(slots[0].transform.GetChild(0).gameObject);
            }

            if (slots[1].transform.childCount > 0) {
                Destroy(slots[1].transform.GetChild(0).gameObject);
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