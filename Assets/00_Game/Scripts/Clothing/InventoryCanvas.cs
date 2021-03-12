using System.Collections.Generic;
using Cred._00_Game.Scripts.Clothing;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cred.Scripts.Clothing {

    //TODO: If you press anything outside of the InventoryButtons, could the InvCanvas act like you pressed the InventoryButton?
    
    public class InventoryCanvas : MonoBehaviour {
        public GameObject[] buttons;
        readonly Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
        InventoryDataHandler inventoryDataHandler;
        [SerializeField] List<ClothingType> tempClothingTypesList = new List<ClothingType>();
        bool hasActivatedScrollView;
        Vector3 newButtonPosition;
        
        public void ToggleButton(GameObject scrollView) {
            var clickButton = EventSystem.current.currentSelectedGameObject;
            clickButton.transform.localPosition = newButtonPosition;
            ToggleButtons(clickButton);
            ToggleScrollView(scrollView, clickButton);
        }

        void ToggleButtons(GameObject clickButton) {
            foreach (var go in buttons) {
                if (go == clickButton) continue;
                go.SetActive(hasActivatedScrollView);
            }
        }

        void ToggleScrollView(GameObject scrollView, GameObject clickButton) {
            if (scrollView.activeInHierarchy) {
                scrollView.SetActive(false);
                clickButton.transform.localPosition = originalPositions[clickButton];
            }
            else {
                scrollView.SetActive(true);
                if (inventoryDataHandler.WearableDictionary.ContainsKey(tempClothingTypesList[0])) {
                    Debug.Log("InventoryHandler " + inventoryDataHandler.WearableDictionary[tempClothingTypesList[0]].Count);
                }
                Debug.Log("ScrollViewActivated " + clickButton.name);
            }
            hasActivatedScrollView = !hasActivatedScrollView;
        }

        public void Start() {
            newButtonPosition = buttons[0].GetComponent<RectTransform>().transform.localPosition;
            foreach (var btn in buttons) {
                originalPositions[btn] = btn.transform.localPosition;
            }
            inventoryDataHandler = GetComponent<InventoryDataHandler>();
        }
    }
}
