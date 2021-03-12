using Cred.Scripts.Clothing;
using EventBrokerFolder;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00_Game.Scripts.Clothing {
    public class InventoryButtonScript : MonoBehaviour, IPointerClickHandler {
        Wearable _wearable;

        public void Setup(Wearable wearable) {
            _wearable = wearable;
            GetComponent<Image>().sprite = wearable.Sprite;
            Debug.Log(wearable.Sprite.name);
        }
        
        public void OnPointerClick(PointerEventData eventData) {
            EventBroker.Instance().SendMessage(new EventClothesChanged(_wearable));
            Debug.Log(_wearable.Sprite.name);
        }
    }
}

