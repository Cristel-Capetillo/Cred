using HUD.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour, IPointerClickHandler {
        readonly Wearable[] wearables = new Wearable[2];
        public GameObject[] clothingItems;
        InventoryButtonScript inventoryButtonScript;
        public Button upcycleConfirmButton;

        public int count = 0;
        public bool bothHasBeenCollected;

        public void FixedUpdate() {
            clothingItems = GameObject.FindGameObjectsWithTag("Clothing");
            if (clothingItems.Length > 0) {
                GetScript();
            }

            if (bothHasBeenCollected) {
                Debug.Log("Wearables: " + wearables[0] + ", " + wearables[1]);
            }
        }

        public void GetScript() {
            for (int i = 0; i < clothingItems.Length; i++) {
                inventoryButtonScript = clothingItems[i].GetComponent<InventoryButtonScript>();

                if (inventoryButtonScript.upcyclingClothingChosen) {
                    count++;

                    if (count == 1) {
                        wearables[0] = inventoryButtonScript._wearable;
                        gameObject.SetActive(true);
                        GetComponent<Image>().sprite = inventoryButtonScript._wearable.Sprite;
                        GetComponentInChildren<Text>().text = inventoryButtonScript._wearable.StylePoints.ToString();
                    }

                    if (count == 2) {
                        wearables[1] = inventoryButtonScript._wearable;
                        bothHasBeenCollected = true;
                        count = 0;
                        upcycleConfirmButton.interactable = true;
                    }

                    inventoryButtonScript.upcyclingClothingChosen = false;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (bothHasBeenCollected) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(wearables[0], wearables[1]));
            }
        }
    }
}