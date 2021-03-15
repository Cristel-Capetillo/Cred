using UnityEngine;

namespace Clothing {
    public class PopUpWindow : MonoBehaviour {

        public bool popupActive;

        public bool isUpCycleWindow;
        public bool isDonateWindow;
        public void OnClickEnterPopUpWindow(GameObject popupWindow)
        {
            popupWindow.SetActive(true);
            popupActive = true;

            if(popupWindow.name == "PopupUpCycleWindow")
            {
                isUpCycleWindow = true;
                isDonateWindow = false;
            }

            else
            {
                isDonateWindow = true;
                isUpCycleWindow = false;
            }
        }

        public void OnClickExit(GameObject popupWindow) {
            popupWindow.SetActive(false);
            popupActive = false;
            isDonateWindow = false;
            isUpCycleWindow = false;

        }
    }
}