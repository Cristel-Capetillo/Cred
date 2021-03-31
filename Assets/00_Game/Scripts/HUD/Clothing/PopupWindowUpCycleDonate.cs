using System;
using Clothing.Upgrade;
using Clothing.Upgrade.Donation;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace HUD.Clothing {
    public class PopupWindowUpCycleDonate : MonoBehaviour {

        [SerializeField] CanvasGroup[] popUpWindows;
        DonationPopUpWarnings donationPopUpWarnings;

        void OnEnable() {
            donationPopUpWarnings = FindObjectOfType<DonationPopUpWarnings>();
        }

        public void OnClickEnterPopUpWindow(CanvasGroup popupWindow) {
            foreach (var window in popUpWindows) {
                if (popupWindow == window) {
                    
                    donationPopUpWarnings.DisableWarning();
                    popupWindow.interactable = !popupWindow.interactable;
                    popupWindow.blocksRaycasts = !popupWindow.blocksRaycasts;
                    EventBroker.Instance().SendMessage(new EventHideUpdateWindows(!popupWindow.interactable));
                    if (popupWindow.interactable) {
                        popupWindow.alpha = 1;
                    }
                    else {
                        popupWindow.alpha = 0;
                    }

                    continue;
                }

                window.blocksRaycasts = false;
                window.alpha = 0;
                window.interactable = false;
            }

            EventBroker.Instance().SendMessage(new EventTogglePopWindow(popupWindow.interactable));
        }
    }
}