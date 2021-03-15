using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        Wearable _wearable;
        public GameObject popupWindow;

        public void Setup(Wearable wearable) {
            _wearable = wearable;
            GetComponent<Image>().sprite = wearable.Sprite;
            gameObject.SetActive(true);
            Debug.Log(wearable.Sprite.name);
        }

        public void OnPointerClick(PointerEventData eventData) {
            // EventBroker.Instance().SendMessage(new EventClothesChanged(_wearable));
            // Debug.Log(_wearable.Sprite.name);

            popupWindow.SetActive(true);
        }

        public void OnClickExit() {
            popupWindow.SetActive(false);
        }
    }
}