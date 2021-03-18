using System.Collections.Generic;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Wearable[] wearables = new Wearable[2];

        public Image slot1;
        public Image slot2;

        public GameObject[] clothingItems;
        public Button upcycleConfirmButton;

        PopupWindowUpCycleDonate popupWindowUpCycleDonate;
        public GameObject popupWindowUpCycle;
        public GameObject content;

        List<InventoryButtonScript> buttonScriptList = new List<InventoryButtonScript>();

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddUpCycleClothes>(GetScript);
            popupWindowUpCycleDonate = GetComponent<PopupWindowUpCycleDonate>();
        }

        public void GetScript(EventAddUpCycleClothes eventAddUpCycleClothes) {
            buttonScriptList.Add(eventAddUpCycleClothes.inventoryButtonScript);
            if (slot1.sprite == null) {
                wearables[0] = eventAddUpCycleClothes.wearable;
                slot1.sprite = eventAddUpCycleClothes.wearable.Sprite;
                slot1.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                return;
            }

            if (slot2.sprite == null) {
                wearables[1] = eventAddUpCycleClothes.wearable;
                slot2.sprite = eventAddUpCycleClothes.wearable.Sprite;
                slot2.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                upcycleConfirmButton.interactable = true;
            }
        }

        public void OnConfirm() {
            if (upcycleConfirmButton.interactable) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(wearables[0], wearables[1]));
                foreach (var wearable in wearables) {
                    wearable.SetAmount(wearable.Amount - 1);
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
        }

        public void CleanUpOnWearableSelect() {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = null;
            upcycleConfirmButton.interactable = false;
        }
    }
}