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
        public Text stylePointText;

        bool popUpWindowIsActive;

        void Start() {
            combinedWearable = GetComponent<CombinedWearables>();

        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventTogglePopWindow>(ValidateShouldDressUp);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventTogglePopWindow>(ValidateShouldDressUp);
        }

        public void Setup(CombinedWearables wearable, PopupWindowUpCycleDonate popupWindow) {
            this.combinedWearable = wearable;
            gameObject.SetActive(true);
            UpdateAmountStylePoint();
        }

        public void UpdateAmountStylePoint() {
            stylePointText.text = combinedWearable.stylePoints.ToString();
        }

        void ValidateShouldDressUp(EventTogglePopWindow popUp) {
            print("here");
            popUpWindowIsActive = popUp.popWindowIsActive;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!popUpWindowIsActive) {
                EventBroker.Instance().SendMessage(new EventClothesChanged(combinedWearable));
                print("I AM CHANGING CLOTHES MAN");
                return;
            }

            EventBroker.Instance().SendMessage(new EventAddToUpgradeSlot(combinedWearable));
        }
    }
}