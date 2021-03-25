using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class ClearSlots : MonoBehaviour {
        public void ClearSlot() {
            if (EventSystem.current.currentSelectedGameObject.transform.childCount > 0) {
                EventBroker.Instance().SendMessage(new EventValidateConfirmButton(false));
                Destroy(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            }
        }
    }
}