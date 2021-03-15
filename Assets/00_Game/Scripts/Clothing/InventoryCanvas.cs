using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Clothing {
    //TODO: If you press anything outside of the InventoryButtons, could the InvCanvas act like you pressed the InventoryButton?

    public class InventoryCanvas : MonoBehaviour {
        [SerializeField] GameObject scrollView;
        [SerializeField] GameObject buttonHolder;
        [SerializeField] Text closeButtonText;
        [SerializeField] InventoryButtonScript inventoryContentPrefab;
        [SerializeField] Transform contentParent;
        readonly List<InventoryButtonScript> inventoryContent = new List<InventoryButtonScript>();
        InventoryDataHandler inventoryDataHandler;
        
        void Start() {
            inventoryDataHandler = GetComponent<InventoryDataHandler>();
        }

        public void ToggleButton(ClothingType clothingType) {
            if (!inventoryDataHandler.WearableDictionary.ContainsKey(clothingType)) {
                Debug.LogWarning($"No item was found in {clothingType.name}");
                return;
            }
            closeButtonText.text = clothingType.name;
            buttonHolder.SetActive(false);
            scrollView.SetActive(true);
            var clothingTypeCount = inventoryDataHandler.WearableDictionary[clothingType].Count;
            ContentPooling(clothingTypeCount);
            AddToInventory(clothingType, clothingTypeCount);
        }

        void AddToInventory(ClothingType clothingType, int clothingTypeCount) {
            for (int i = 0; i < clothingTypeCount; i++) {
                    inventoryContent[i].Setup(inventoryDataHandler.WearableDictionary[clothingType][i]);
            }
        }

        void ContentPooling(int clothingTypeCount) {
            if (inventoryContent.Count < clothingTypeCount) {
                var temp = Mathf.Max(0, inventoryContent.Count - 1);
                for (int i = temp; i < clothingTypeCount; i++) {
                    inventoryContent.Add(Instantiate(inventoryContentPrefab, contentParent));
                }
            }
            else {
                for (int i = clothingTypeCount; i < inventoryContent.Count; i++) {
                    inventoryContent[i].gameObject.SetActive(false);
                }
            }
        }

        public void CloseScrollview() {
            buttonHolder.SetActive(true);
            scrollView.SetActive(false);
        }
    }
}