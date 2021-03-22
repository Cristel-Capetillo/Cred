using System.Collections.Generic;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Dictionary<Image, Wearable> wearables = new Dictionary<Image, Wearable>();

        public Image slot1;
        public Image slot2;

        public GameObject[] clothingItems;
        public Button upcycleConfirmButton;

        PopupWindowUpCycleDonate popupWindowUpCycleDonate;
        public GameObject popupWindowUpCycle;
        public GameObject content;

        List<InventoryButtonScript> buttonScriptList = new List<InventoryButtonScript>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddUpCycleClothes>(AssignUpCycleSlot);
            popupWindowUpCycleDonate = GetComponent<PopupWindowUpCycleDonate>();
            wearables[slot1] = null;
            wearables[slot2] = null;
        }

        public void AssignUpCycleSlot(EventAddUpCycleClothes eventAddUpCycleClothes) {
            buttonScriptList.Add(eventAddUpCycleClothes.inventoryButtonScript);
            if (slot1.sprite == null) {
                if (wearables[slot2] != null && wearables[slot2].ClothingType != eventAddUpCycleClothes.wearable.ClothingType) return;
                wearables[slot1] = eventAddUpCycleClothes.wearable;
                slot1.sprite = eventAddUpCycleClothes.wearable.Sprite;
                slot1.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                wearables[slot1].SetAmount(-1);
                return;
            }

            if (slot2.sprite == null) {
                if (wearables[slot1] != null && wearables[slot1].ClothingType != eventAddUpCycleClothes.wearable.ClothingType) return;
                wearables[slot2] = eventAddUpCycleClothes.wearable;
                slot2.sprite = eventAddUpCycleClothes.wearable.Sprite;
                slot2.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                upcycleConfirmButton.interactable = true;
                wearables[slot2].SetAmount(-1);
            }
        }

        public void OnConfirm() {
            if (upcycleConfirmButton.interactable) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(wearables[slot1], wearables[slot2]));
                foreach (var wearable in wearables) {
                    wearable.Value.SetAmount(wearable.Value.Amount - 1);
                    Debug.Log($"Decreased {wearable} amount by 1");
                }

                foreach (var buttonScript in buttonScriptList) {
                    buttonScript.UpdateAmountStylePoint();
                }
                CleanUpOnExitAndConfirm();
            }
        }

        public void CleanUpOnExitAndConfirm() {
            popupWindowUpCycle.SetActive(false);
            popupWindowUpCycleDonate.ResetBools();
            slot1.sprite = null;
            slot1.GetComponentInChildren<Text>().text = null;
            slot2.sprite = null;
            slot2.GetComponentInChildren<Text>().text = null;
            buttonScriptList.Clear();
            wearables[slot1].SetAmount(1);
            wearables[slot1] = null;
            wearables[slot2].SetAmount(1);
            wearables[slot2] = null;
        }

        public void CleanUpOnWearableSelect() {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = null;
            upcycleConfirmButton.interactable = false;
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>() == slot1) {
                wearables[slot1].SetAmount(1);
                wearables[slot1] = null;
            }
            else {
                wearables[slot2].SetAmount(1);
                wearables[slot2] = null;
            }
        }
    }
}