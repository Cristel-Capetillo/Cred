using System;
using System.Collections.Generic;
using Clothing.DressUp;
using Currency.Coins;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class BuyNotOwnedClothes : MonoBehaviour {
        [SerializeField] GameObject buyNotOwnedClothes;
        ClothingManager clothingManager;

        void Awake() {
            clothingManager = FindObjectOfType<ClothingManager>();
        }

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
            var price = clothingManager.GetPrice(wearable.rarity);
            if (coins.Coins >= price) {
                coins.Coins -= price;
                UnityEngine.Analytics.Analytics.CustomEvent("Store", new Dictionary<string, object> {
                    {"Purchased", wearable.ToString()}
                });
                EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(wearable, 1));
                EventBroker.Instance().SendMessage(new EventUpdateWearableHud());
            }
        }

        //TODO: MOVE TO A MAKE SENSE PLACE
        
    }
}