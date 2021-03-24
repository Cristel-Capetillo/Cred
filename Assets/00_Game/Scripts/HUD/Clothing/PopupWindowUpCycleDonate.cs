using Clothing.Upgrade;
using UnityEngine;
using Utilities;

namespace HUD.Clothing {
    public class PopupWindowUpCycleDonate : MonoBehaviour {
        UpcycleWearables upCycleWearables;

        [SerializeField] GameObject[] popUpWindows;

        void Start() {
            upCycleWearables = GetComponent<UpcycleWearables>();
        }

        public void OnClickEnterPopUpWindow(GameObject popupWindow) {
            foreach (var window in popUpWindows) {
                if (popupWindow == window) {
                    window.SetActive(!window.activeSelf);
                    continue;
                }

                window.SetActive(false);
            }

            EventBroker.Instance().SendMessage(new EventTogglePopWindow(popupWindow.activeSelf));
        }
    }
}