using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        Wearable _wearable;
        PopUpWindow _popupWindow;
        Wearable _chosenWearable1;
        Wearable _chosenWearable2;

        public bool hasClickedWearable;
        public bool chosenFirstWearable = false;
        public bool chosenSecondWearable = false;

        private void Awake()
        {
            _popupWindow = GameObject.FindGameObjectWithTag("ClothingInventory").GetComponent<PopUpWindow>();
        }
        public void Setup(Wearable wearable) {
            _wearable = wearable;
            GetComponent<Image>().sprite = wearable.Sprite;
            gameObject.SetActive(true);
            Debug.Log(wearable.Sprite.name);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!_popupWindow.popupActive)
            {
                _chosenWearable1 = _wearable;
                EventBroker.Instance().SendMessage(new EventClothesChanged(_chosenWearable1));
                Debug.Log(_wearable.Sprite.name);
            }
            else
            {

                if (_popupWindow.isUpCycleWindow && !chosenFirstWearable)
                {
                    _chosenWearable1 = _wearable;
                    chosenFirstWearable = true;

                    Debug.Log("First Chosen Wearable: " + _chosenWearable1);

                }

                if(_popupWindow.isUpCycleWindow && !chosenSecondWearable)
                {
                    _chosenWearable2 = _wearable;
                    chosenSecondWearable = true;
                    Debug.Log("Second Chosen Wearable: " + _chosenWearable2);
                }
               

                if (/*_popupWindow.isUpCycleWindow && */chosenFirstWearable && chosenSecondWearable) { 

                    EventBroker.Instance().SendMessage(new MessageUpCycleClothes(_chosenWearable1, _chosenWearable2));
                }


                if (_popupWindow.isDonateWindow)
                {
                    Debug.Log("Donate is Active");
                    EventBroker.Instance().SendMessage(new MessageDonateClothes(_wearable));
                }

            }
            hasClickedWearable = false;

        }




    }
}