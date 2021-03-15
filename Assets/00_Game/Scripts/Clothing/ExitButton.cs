using UnityEngine;

namespace Clothing {
    public class ExitButton : MonoBehaviour {
        public GameObject popupWindow;
        
        public void OnClickExit() {
            popupWindow.SetActive(false);
        }
    }
}