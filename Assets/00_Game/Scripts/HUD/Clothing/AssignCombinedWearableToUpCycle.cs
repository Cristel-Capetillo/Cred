using System;
using Clothing;
using Clothing.Inventory;
using Clothing.Upgrade;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace HUD.Clothing {
    public class AssignCombinedWearableToUpCycle : MonoBehaviour, IPointerClickHandler {
        [FormerlySerializedAs("combineWearable")]
        CombinedWearables combinedWearable;
        PopupWindowUpCycleDonate _popupWindow;
        //public bool upcyclingClothingChosen;
        public Text stylePointText;
        //public Text amountText;

        bool popUpWindowIsActive;
        void Start() {
            combinedWearable = GetComponent<CombinedWearables>();
            
            //what wearable in "combinedWearable" in slot1 (orange)
            //what wearable in "combinedWearable" in slot2 (black)
            
            //generate new combined wearable (orange + black)
            
            //send message new item created
            //send message subtract from slot1
            //send message subtract from slot2
            EventBroker.Instance().SubscribeMessage<EventTogglePopWindow>(ValidateShouldDressUp);
        }

        public void Setup(CombinedWearables wearable, PopupWindowUpCycleDonate popupWindow) {
            this.combinedWearable = wearable;
            gameObject.SetActive(true);
            //GetComponent<Image>().sprite = wearable.Sprite;
            UpdateAmountStylePoint();
            //print(wearable.stylePoints + " " + wearable.Rarity.name);
            _popupWindow = popupWindow;
            LockedUpcycledWearables();
        }

        public void UpdateAmountStylePoint() {
            stylePointText.text = combinedWearable.stylePoints.ToString();
            //TODO amount is not in wearable
            //amountText.text = combinedWearable.stylePoints.ToString();
        }

        void ValidateShouldDressUp(EventTogglePopWindow popUp) {
            popUpWindowIsActive = popUp.popWindowIsActive;
        }

        public void LockedUpcycledWearables() {
            // if (!wearable.HasUnlockedUpCycledWearable() && wearable.isUpCycledWearable)
            // {
            //     gameObject.SetActive(false);
            // }
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!popUpWindowIsActive) {
                //EventBroker.Instance().SendMessage(new EventClothesChanged(combinedWearable));
                print("I AM CHANGING CLOTHES MAN");
                return;
            }
            
            EventBroker.Instance().SendMessage(new EventAddToUpgradeSlot(combinedWearable));
        }
    }
}