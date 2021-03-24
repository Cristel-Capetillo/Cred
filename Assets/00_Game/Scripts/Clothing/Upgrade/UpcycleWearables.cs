using System.Collections.Generic;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Dictionary<Image, Wearable> combineWearablesDic = new Dictionary<Image, Wearable>();

        public Image slot1;
        public Image slot2;

        public GameObject[] clothingItems;
        public Button upcycleConfirmButton;
        public GameObject warningUpcycleText;

        PopupWindowUpCycleDonate popupWindowUpCycleDonate;
        public GameObject popupWindowUpCycle;
        public GameObject content;

        List<AssignCombinedWearableToUpCycle> buttonScriptList = new List<AssignCombinedWearableToUpCycle>();

        bool cannotUpcycle;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddUpCycleClothes>(AssignUpCycleSlot);
            popupWindowUpCycleDonate = GetComponent<PopupWindowUpCycleDonate>();
            combineWearablesDic[slot1] = null;
            combineWearablesDic[slot2] = null;
        }

        public void AssignUpCycleSlot(EventAddUpCycleClothes eventAddUpCycleClothes) {
            buttonScriptList.Add(eventAddUpCycleClothes.assignCombinedWearableToUpCycle);
            // if (slot1.sprite == null) {
            //     if (combineWearablesDic[slot2] != null && combineWearablesDic[slot2].BodyPart != eventAddUpCycleClothes.combinedWearable.BodyPart) return;
            //     combineWearablesDic[slot1] = eventAddUpCycleClothes.combinedWearable;
            //     slot1.sprite = eventAddUpCycleClothes.combinedWearable.Sprite;
            //     slot1.GetComponentInChildren<Text>().text = eventAddUpCycleClothes..ToString();
            //     //wearables[slot1].SetAmount(-1);
            //     return;
            // }
            //
            // if (slot2.sprite == null) {
            //     if (combineWearablesDic[slot1] != null && combineWearablesDic[slot1].BodyPart != eventAddUpCycleClothes.combinedWearable.BodyPart) return;
            //     combineWearablesDic[slot2] = eventAddUpCycleClothes.combinedWearable;
            //     slot2.sprite = eventAddUpCycleClothes.combinedWearable.Sprite;
            //     slot2.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.combinedWearable.StylePoints.ToString();
            //     //wearables[slot2].SetAmount(-1);
            // }
            //
            // if(combineWearablesDic[slot1].Rarity != combineWearablesDic[slot2].Rarity)
            // {
            //     if (!cannotUpcycle)
            //     {
            //         cannotUpcycle = true;
            //     }
            // }
            // else
            // {
            //     cannotUpcycle = false;
            // }
            //
            // SpawnWarningText();

        }

        void SpawnWarningText()
        {
            if (cannotUpcycle)
            {
                warningUpcycleText.SetActive(true);
            }
            else
            {
                warningUpcycleText.SetActive(false);
                upcycleConfirmButton.interactable = true;
            }
        }

        public void OnConfirm() {
            if (upcycleConfirmButton.interactable) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(combineWearablesDic[slot1], combineWearablesDic[slot2]));

                foreach (var buttonScript in buttonScriptList) {
                    buttonScript.UpdateAmountStylePoint();
                }
                CleanUpOnExitAndConfirm();
            }
        }

        public void CleanUpOnExitAndConfirm() {
            popupWindowUpCycle.SetActive(false);
            //popupWindowUpCycleDonate.ResetBools();
            slot1.sprite = null;
            slot1.GetComponentInChildren<Text>().text = null;
            slot2.sprite = null;
            slot2.GetComponentInChildren<Text>().text = null;
            buttonScriptList.Clear();
           // wearables[slot1].SetAmount(1);
            combineWearablesDic[slot1] = null;
            //wearables[slot2].SetAmount(1);
            combineWearablesDic[slot2] = null;
        }

        public void CleanUpOnWearableSelect() {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = null;
            upcycleConfirmButton.interactable = false;
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>() == slot1) {
               // wearables[slot1].SetAmount(1);
                combineWearablesDic[slot1] = null;
            }
            else {
              //  wearables[slot2].SetAmount(1);
                combineWearablesDic[slot2] = null;
            }
        }
    }
}