using System;
using Clothing.Upgrade.UpCycle;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Random = UnityEngine.Random;

namespace Clothing.Inventory {
    public class PurchaseClothingConfirmationMenu : MonoBehaviour{
        public Button buyButton;
        [SerializeField] Button closeButton;
        [SerializeField] Text purchaseItemText;
        [SerializeField] string purchaseTextPrefix;
        [SerializeField] float sizeToDisplayReward = 4f;
        [SerializeField] RectTransform rectTransform;
        
        void Start() {
            foreach (var clothingsInConfirmationMenu in FindObjectsOfType<PurchaseClothingConfirmationMenu>()) {
                if (clothingsInConfirmationMenu != this) {
                    Destroy(clothingsInConfirmationMenu.gameObject);
                }
            }
            closeButton.onClick.AddListener(() => Destroy(gameObject));
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(CloseThis);
        }

        public void Setup(CombinedWearables combinedWearables) {
            purchaseItemText.text = $"{purchaseTextPrefix} {combinedWearables.rarity.name} {combinedWearables.clothingType.SingularName}?";
            buyButton.GetComponentInChildren<Text>().text = BuyNotOwnedClothes.GetPrice(combinedWearables.rarity).ToString();
            ShowItemToBuy(combinedWearables);
        }

        void CloseThis(EventUpdatePlayerInventory eventUpdatePlayerInventory) {
            Destroy(gameObject);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdatePlayerInventory>(CloseThis);
        }

        void ShowItemToBuy(CombinedWearables reward) {
            var instance = Instantiate(reward.gameObject, rectTransform);
            for (var i = 1; i < instance.transform.childCount; i++) {
                instance.transform.GetChild(i).gameObject.SetActive(false);
            }
            Resize(instance, sizeToDisplayReward);
            instance.GetComponent<AssignCombinedWearableToUpCycle>().enabled = false;
        }

        void Resize(GameObject go, float newScale) {
            var newSize = new Vector2(newScale, newScale);
            go.GetComponent<RectTransform>().localScale = newSize;
        }
    }
}