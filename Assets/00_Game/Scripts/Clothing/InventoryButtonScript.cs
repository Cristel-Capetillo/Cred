using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        Wearable _wearable;

        public void Setup(Wearable wearable) {
            _wearable = wearable;
            gameObject.SetActive(true);
            GetComponent<Image>().sprite = wearable.Sprite;
            GetComponentInChildren<Text>().text = wearable.StylePoints.ToString();
            print(wearable.StylePoints + " " + wearable.Rarity.name);
        }

        public void OnPointerClick(PointerEventData eventData) {
            EventBroker.Instance().SendMessage(new EventClothesChanged(_wearable));
        }
    }
}