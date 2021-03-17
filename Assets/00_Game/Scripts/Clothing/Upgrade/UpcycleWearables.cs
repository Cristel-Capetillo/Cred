using HUD.Clothing;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade {
    public class UpcycleWearables : MonoBehaviour {
        readonly Wearable[] wearables = new Wearable[2];

        public Image slot1;
        public Image slot2;

        public GameObject[] clothingItems;
        InventoryButtonScript inventoryButtonScript;
        public Button upcycleConfirmButton;

        public int count = 0;
        public bool bothHasBeenCollected;

        public void FixedUpdate() {

            if (clothingItems.Length > 0) GetScript();
        }

        public void FindClothes()
        {
            clothingItems = GameObject.FindGameObjectsWithTag("Clothing");
        }

        public void GetScript() {
            for (var i = 0; i < clothingItems.Length; i++) {
                inventoryButtonScript = clothingItems[i].GetComponent<InventoryButtonScript>();

                if (inventoryButtonScript.upcyclingClothingChosen) {
                    count++;

                    if (count == 1) {
                        wearables[0] = inventoryButtonScript._wearable;
                        slot1.GetComponent<Image>().sprite = inventoryButtonScript._wearable.Sprite;
                        slot1.GetComponentInChildren<Text>().text = inventoryButtonScript._wearable.StylePoints.ToString();
                    }

                    if (count == 2) {
                        wearables[1] = inventoryButtonScript._wearable;
                        bothHasBeenCollected = true;
                        count = 0;
                        upcycleConfirmButton.interactable = true;
                        slot2.GetComponent<Image>().sprite = inventoryButtonScript._wearable.Sprite;
                        slot2.GetComponentInChildren<Text>().text = inventoryButtonScript._wearable.StylePoints.ToString();
                    }

                    inventoryButtonScript.upcyclingClothingChosen = false;
                }
            }
        }

        public void OnConfirm() {
            if (upcycleConfirmButton.interactable) {
                EventBroker.Instance().SendMessage(new MessageUpCycleClothes(wearables[0], wearables[1]));
                print("HasConfirmed " + wearables[0] + wearables[1]);
            }
        }
    }
}