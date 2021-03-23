using System.Collections.Generic;
using Clothing;
using Clothing.Upgrade;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
namespace HUD.Clothing {
    //TODO: If you press anything outside of the InventoryButtons, could the InvCanvas act like you pressed the InventoryButton?

    public class InventoryCanvas : MonoBehaviour {
        [SerializeField] GameObject scrollView;
        [SerializeField] GameObject buttonHolder;
        [SerializeField] Text closeButtonText;
        public InventoryButtonScript inventoryContentPrefab;
        [SerializeField] Transform contentParent;

        [SerializeField] PopupWindowUpCycleDonate popupWindonwUpcycleDonate;

        readonly List<InventoryButtonScript> inventoryContent = new List<InventoryButtonScript>();
        InventoryDataHandler inventoryDataHandler;

        public int InventoryContentCount => inventoryContent.Count;

        void Start() {
            inventoryDataHandler = GetComponent<InventoryDataHandler>();
            EventBroker.Instance().SubscribeMessage<MessageUpCycleClothes>(UpCycleWearable);

        }

        public void ToggleButton(BodyPart bodyPart) {
            if (!inventoryDataHandler.wearableDictionary.ContainsKey(bodyPart)) {
                Debug.LogWarning($"No item was found in {bodyPart.name}");
                return;
            }

            closeButtonText.text = bodyPart.name;
            buttonHolder.SetActive(false);
            scrollView.SetActive(true);
            ContentPooling(bodyPart);
            AddToInventory(bodyPart);
        }

        public void CloseScrollview() {
            buttonHolder.SetActive(true);
            scrollView.SetActive(false);
        }

        void AddToInventory(BodyPart bodyPart) {
            for (int i = 0; i < inventoryDataHandler.wearableDictionary[bodyPart].Count; i++) {
                inventoryContent[i].Setup(inventoryDataHandler.wearableDictionary[bodyPart][i], popupWindonwUpcycleDonate);
            }
        }

        void ContentPooling(BodyPart bodyPart) {
            var clothingTypeCount = inventoryDataHandler.wearableDictionary[bodyPart].Count;
        
            if (inventoryContent.Count < clothingTypeCount) {
                for (int i = inventoryContent.Count; i < clothingTypeCount; i++) {
                    inventoryContent.Add(Instantiate(inventoryContentPrefab, contentParent));
                }
            } else {
                for (int i = clothingTypeCount; i < inventoryContent.Count; i++) {
                    
                    inventoryContent[i].gameObject.SetActive(false);
                  
                }
            }

        }

        void UpCycleWearable(MessageUpCycleClothes messageUpCycleClothes)
        {
           for(int i = 0; i < inventoryContent.Count; i++)
            {
              
            }
        }
    }
}