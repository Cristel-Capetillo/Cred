using System;
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
        InventoryButtonScript inventoryButtonScript;
        public Button upcycleConfirmButton;

        public int count = 0;
        public bool bothHasBeenCollected;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddUpCycleClothes>(GetScript);
        }

        public void GetScript(EventAddUpCycleClothes eventAddUpCycleClothes) {
            count++;
            switch (count) {
                case 1:
                    wearables[0] = eventAddUpCycleClothes.wearable;
                    slot1.GetComponent<Image>().sprite = eventAddUpCycleClothes.wearable.Sprite;
                    slot1.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                    break;
                case 2:
                    wearables[1] = eventAddUpCycleClothes.wearable;
                    bothHasBeenCollected = true;
                    count = 0;
                    upcycleConfirmButton.interactable = true;
                    slot2.GetComponent<Image>().sprite = eventAddUpCycleClothes.wearable.Sprite;
                    slot2.GetComponentInChildren<Text>().text = eventAddUpCycleClothes.wearable.StylePoints.ToString();
                    break;
            }
        }

        public void OnConfirm() {
            if (upcycleConfirmButton.interactable) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(wearables[0], wearables[1]));
                print("HasConfirmed " + wearables[0] + wearables[1]);
            }
        }
    }
}