﻿using UnityEngine;

namespace Clothing.Upgrade {
    public class PopupWindowUpCycleDonate : MonoBehaviour {
        public bool popupActive;

        public bool isUpCycleWindow;
        public bool isDonateWindow;

        public void OnClickEnterPopUpWindow(GameObject popupWindow) {
            popupWindow.SetActive(true);
            popupActive = true;

            if (popupWindow.name == "PopupWindowUpCycle") {
                isUpCycleWindow = true;
                isDonateWindow = false;
            } else {
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