using Clothing.Upgrade;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace HUD.Clothing {
    public class PopupWindowUpCycleDonate : MonoBehaviour {

        [SerializeField] CanvasGroup[] popUpWindows;

        
        public void OnClickEnterPopUpWindow(CanvasGroup popupWindow) {
            foreach (var window in popUpWindows) {
                if (popupWindow == window) {
                    print(window);
                    popupWindow.interactable = !popupWindow.interactable;
                    popupWindow.blocksRaycasts = !popupWindow.blocksRaycasts;
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