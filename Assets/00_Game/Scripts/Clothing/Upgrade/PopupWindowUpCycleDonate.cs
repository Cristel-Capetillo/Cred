using System;
using UnityEngine;

namespace Clothing.Upgrade {
    public class PopupWindowUpCycleDonate : MonoBehaviour {
        public bool popupActive;

        public bool isUpCycleWindow;
        public bool isDonateWindow;

        UpcycleWearables upcycleWearables;
        bool popupWindowIsActive;

        void Start() {
            upcycleWearables = GetComponent<UpcycleWearables>();
        }

        public void OnClickEnterPopUpWindow(GameObject popupWindow) {
            if (!popupWindowIsActive) {
                popupWindow.SetActive(true);
                popupActive = true;
                popupWindowIsActive = true;

                if (popupWindow.name == "UpCyclePopupWindow") {
                    isUpCycleWindow = true;
                    isDonateWindow = false;
                } else {
                    isDonateWindow = true;
                    isUpCycleWindow = false;
                }
            }
        }

        public void OnClickExit(GameObject popupWindow) {
            popupWindow.SetActive(false);
            ResetBools();
            upcycleWearables.CleanUpOnExitAndConfirm();
        }

        public void ResetBools() {
            popupActive = false;
            isDonateWindow = false;
            isUpCycleWindow = false;
            popupWindowIsActive = false;
        }
    }
}