using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing {
    public class CombinedUI : MonoBehaviour {
        public Image unlockImage;
        public Text amountText;
        public Image[] colorData;

       public void UpdateUI(CombinedWearables combinedWearables) {
            foreach (var image in colorData) {
                image.enabled = false;
            }

            for (var i = 0; i < combinedWearables.wearable.Count - 1; i++) {
                switch (i) {
                    case 0:
                        colorData[0].enabled = true;
                        colorData[0].color = combinedWearables.wearable[0].colorData.color;
                        break;
                    case 1:
                        colorData[1].enabled = true;
                        colorData[1].color = combinedWearables.wearable[1].colorData.color;
                        break;
                    case 2:
                        colorData[2].enabled = true;
                        colorData[2].color = combinedWearables.wearable[2].colorData.color;
                        break;
                }
            }
        }
    }
}