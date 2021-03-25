using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Clothing.Upgrade {
    public class ClearSlots : MonoBehaviour {
        public void ClearSlot() {
            var go = EventSystem.current.currentSelectedGameObject;
            if (go.transform.childCount > 0) {
                EventBroker.Instance().SendMessage(new EventValidateConfirmButton(false, go.transform.GetChild(0).GetComponent<CombinedWearables>()));
                Destroy(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            }
        }
    }
}