using Currency.Coins;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class BuyNotOwnedClothes : MonoBehaviour {
        [SerializeField] GameObject buyNotOwnedClothes;
        
        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventBuyNotOwnedClothes>(OpenBuyClothesMenu);
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventBuyNotOwnedClothes>(OpenBuyClothesMenu);
        }
        
        void OpenBuyClothesMenu(EventBuyNotOwnedClothes eventBuyNotOwnedClothes) {
            var instance = Instantiate(buyNotOwnedClothes, FindObjectOfType<PlayerInventory>().GetComponentInChildren<Canvas>().transform);
            instance.GetComponent<PurchaseClothingConfirmationMenu>().Setup(eventBuyNotOwnedClothes.CombinedWearables);
            instance.GetComponent<PurchaseClothingConfirmationMenu>().buyButton.onClick.AddListener(() => BuyClothes(eventBuyNotOwnedClothes.CombinedWearables));
        }
        
        void BuyClothes(CombinedWearables wearable) {
            var coins = FindObjectOfType<Coin>();
            var price = GetPrice(wearable.rarity);
            if (coins.Coins >= price) {
                coins.Coins -= price;
                EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearable, 1));
            }
        }
        
        //TODO: MOVE TO A MAKE SENSE PLACE
        public static int GetPrice(Rarity rarity) {
            return rarity.name switch {
                "Basic" => 300,
                "Normal" => 500,
                "Designer" => 800,
                _ => -1
            };
        }
    }
}
