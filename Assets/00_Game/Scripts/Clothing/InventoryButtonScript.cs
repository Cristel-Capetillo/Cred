using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
<<<<<<< HEAD
        public Wearable _wearable;
        PopUpWindow popupWindow;

        public bool hasBeenChosen;

        private void Awake()
        {
            popupWindow = GameObject.FindGameObjectWithTag("ClothingInventory").GetComponent<PopUpWindow>();
        }
=======
        Wearable _wearable;

>>>>>>> 3b74d04ae2e8b0046f7ac7a55671cdb8008f2e8a
        public void Setup(Wearable wearable) {
            _wearable = wearable;
            gameObject.SetActive(true);
            GetComponent<Image>().sprite = wearable.Sprite;
            GetComponentInChildren<Text>().text = wearable.StylePoints.ToString();
            print(wearable.StylePoints + " " + wearable.Rarity.name);
        }

        public void OnPointerClick(PointerEventData eventData) {
<<<<<<< HEAD
            
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



      
=======
            EventBroker.Instance().SendMessage(new EventClothesChanged(_wearable));
        }
>>>>>>> 3b74d04ae2e8b0046f7ac7a55671cdb8008f2e8a
    }
}