using System.Collections.Generic;
using Clothing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IconUpdate : MonoBehaviour
{
       CombinedWearables combinedWearables;
       public Image[] images;
       public GameObject[] gameObject;
       
       void Start() {
              combinedWearables = GetComponent<CombinedWearables>();
              images = GetComponentsInChildren<Image>();
              DisableImages();
              UpdateImages();
       }

       void DisableImages() {
              images[1].enabled = false;
              images[2].enabled = false;
              images[3].enabled = false;
       }

       void UpdateImages() {
              Debug.Log("CombinedCount: " + combinedWearables.wearable.Count + " ImagesLength: " + images.Length);
              switch (combinedWearables.wearable.Count) {
                     case 1:
                            images[1].sprite = combinedWearables.wearable[0].Sprite;
                            Debug.Log("Update case1");
                            ActivateImage(1);
                            break;
                     case 2:
                            images[2].sprite = combinedWearables.wearable[0].Sprite;
                            images[3].sprite = combinedWearables.wearable[1].Sprite;
                            Debug.Log("Update case2");
                            ActivateImage(2);
                            break;
                     case 3:
                            images[1].sprite = combinedWearables.wearable[0].Sprite;
                            images[2].sprite = combinedWearables.wearable[1].Sprite;
                            images[3].sprite = combinedWearables.wearable[2].Sprite;
                            Debug.Log("Update case3");
                            ActivateImage(3);
                            break;
              }
       }

       void ActivateImage(int amountToActivate) {
              switch (amountToActivate) {
                     case 1:
                            images[1].enabled = true;
                            Debug.Log("Activate case1");
                            break;
                     case 2:
                            images[2].enabled = true;
                            images[3].enabled = true;
                            Debug.Log("Activate case2");
                            break;
                     case 3:
                            images[1].enabled = true;
                            images[2].enabled = true;
                            images[3].enabled = true;
                            Debug.Log("Activate case3");
                            break;
              }
       }
}
