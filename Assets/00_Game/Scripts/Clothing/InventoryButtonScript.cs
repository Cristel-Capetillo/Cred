using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        public Wearable _wearable;
        PopUpWindow popupWindow;

        public bool hasBeenChosen;

        private void Awake()
        {
            popupWindow = GameObject.FindGameObjectWithTag("ClothingInventory").GetComponent<PopUpWindow>();
        }
        public void Setup(Wearable wearable) {
            _wearable = wearable;
            GetComponent<Image>().sprite = wearable.Sprite;
            gameObject.SetActive(true);
            Debug.Log(wearable.Sprite.name);
        }

        public void OnPointerClick(PointerEventData eventData) {
            
            if (!popupWindow.popupActive)
            {
                EventBroker.Instance().SendMessage(new EventClothesChanged(_wearable));
                Debug.Log(_wearable.Sprite.name);
            }
            else
            {
                if (popupWindow.isUpCycleWindow)
                {
                    hasBeenChosen = true;
                     Debug.Log("PopUp UpCycle is Active");
                       // EventBroker.Instance().SendMessage(new MessageUpCycleClothes(_wearable, _wearable2));
                    
                 
                }

                if (popupWindow.isDonateWindow)
                {
                    Debug.Log("Donate is Active");
                    EventBroker.Instance().SendMessage(new MessageDonateClothes(_wearable));
                }

            }
        }



      
    }
}