using System;
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
        
        void Start() {
            foreach (var VARIABLE in FindObjectsOfType<PurchaseClothingConfirmationMenu>()) {
                if (VARIABLE != this) {
                    Destroy(VARIABLE.gameObject);
                }
            }
            closeButton.onClick.AddListener(() => Destroy(gameObject));
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(CloseThis);
        }

        public void Setup(CombinedWearables combinedWearables) {
            purchaseItemText.text = $"{purchaseTextPrefix} {combinedWearables.rarity.name} {combinedWearables.clothingType.SingularName}?";
            buyButton.GetComponentInChildren<Text>().text = BuyNotOwnedClothes.GetPrice(combinedWearables.rarity).ToString();
        }

        void CloseThis(EventUpdatePlayerInventory eventUpdatePlayerInventory) {
            Destroy(gameObject);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdatePlayerInventory>(CloseThis);
        }
    }
}