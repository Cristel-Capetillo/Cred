using Clothing;
using Clothing.Upgrade;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace HUD.Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        public Wearable wearable;
        PopupWindowUpCycleDonate _popupWindow;
        public bool upcyclingClothingChosen;
        public Text stylePointText;
        public Text amountText;

        public void Setup(Wearable wearable, PopupWindowUpCycleDonate popupWindow) {

                this.wearable = wearable;
                gameObject.SetActive(true);
                GetComponent<Image>().sprite = wearable.Sprite;
                UpdateAmountStylePoint();
                print(wearable.StylePoints + " " + wearable.Rarity.name);
                _popupWindow = popupWindow;
                LockedUpcycledWearables();
            
        }

        public void UpdateAmountStylePoint() {
            stylePointText.text = wearable.StylePoints.ToString();
            amountText.text = wearable.Amount.ToString();
        }
        public void LockedUpcycledWearables()
        {
            if (!wearable.HasUnlockedUpCycledWearable() && wearable.isUpCycledWearable)
            {
                gameObject.SetActive(false);
            }
           
        }
            
        public void OnPointerClick(PointerEventData eventData) {
            if (!wearable.Unlocked()) {
                Debug.Log("Not unlocked.");
                return;
            }
            if (!_popupWindow.popupActive) {
                EventBroker.Instance().SendMessage(new EventClothesChanged(wearable));
                Debug.Log(wearable.Sprite.name);
            }
            else {
                if (_popupWindow.isUpCycleWindow) {
                    EventBroker.Instance().SendMessage(new EventAddUpCycleClothes(wearable, this));
                    upcyclingClothingChosen = true;
                }

                if (_popupWindow.isDonateWindow) {
                    Debug.Log("Donate is Active");
                    EventBroker.Instance().SendMessage(new MessageDonateClothes(wearable));
                }
            }
        }
    }
}