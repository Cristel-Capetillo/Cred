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
           Debug.Log("UpdateUI");
           foreach (var image in colorData) {
                image.enabled = false;
                Debug.Log("foreach"); 
           }
           var stylePoints = combinedWearables.stylePoints;
           foreach (var wearable in combinedWearables.wearable) {
               
           }


           for (var i = 0; i < combinedWearables.wearable.Count - 1; i++) {
                Debug.Log("for");
                switch (i) {
                    case 0:
                        colorData[0].enabled = true;
                        colorData[0].color = combinedWearables.wearable[0].colorData.color;
                        Debug.Log("case0 " + combinedWearables.wearable[0].colorData.name);
                        break;
                    case 1:
                        colorData[1].enabled = true;
                        colorData[1].color = combinedWearables.wearable[1].colorData.color;
                        Debug.Log("case1 " + combinedWearables.wearable[0].colorData.name);             
                        break;
                    case 2:
                        colorData[2].enabled = true;
                        colorData[2].color = combinedWearables.wearable[2].colorData.color;
                        Debug.Log("case2 " + combinedWearables.wearable[0].colorData.name);
                        break;
                }
            }
        }
    }
}