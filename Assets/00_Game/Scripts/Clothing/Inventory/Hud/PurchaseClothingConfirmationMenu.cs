using System;
using Clothing.DressUp;
using Clothing.Upgrade;
using Clothing.Upgrade.UpCycle;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Random = UnityEngine.Random;

namespace Clothing.Inventory {
    public class PurchaseClothingConfirmationMenu : MonoBehaviour {
        public Button buyButton;
        [SerializeField] Button closeButton;
        [SerializeField] Text purchaseItemText;
        [SerializeField] string purchaseTextPrefix;
        [SerializeField] float sizeToDisplayReward = 4f;
        [SerializeField] RectTransform rectTransform;
        ClothingManager clothingManager;

        void Awake() {
            foreach (var clothingsInConfirmationMenu in FindObjectsOfType<PurchaseClothingConfirmationMenu>()) {
                if (clothingsInConfirmationMenu != this) {
                    Destroy(clothingsInConfirmationMenu.gameObject);
                }
            }

            clothingManager = FindObjectOfType<ClothingManager>();
            closeButton.onClick.AddListener(() => Destroy(gameObject));
            EventBroker.Instance().SubscribeMessage<EventUpdatePlayerInventory>(OnEventUpdateInventory);
            EventBroker.Instance().SubscribeMessage<EventHideUpdateWindows>(OnHideMenu);
        }

        public void Setup(CombinedWearables combinedWearables) {
            purchaseItemText.text = $"{purchaseTextPrefix} {combinedWearables.rarity.name} {combinedWearables.clothingType.SingularName}?";
            buyButton.GetComponentInChildren<Text>().text = clothingManager.GetPrice(combinedWearables.rarity).ToString();
            ShowItemToBuy(combinedWearables);
        }

        void CloseThis() {
            Destroy(gameObject);
        }

        void OnHideMenu(EventHideUpdateWindows hide)
            => CloseThis();


        void OnEventUpdateInventory(EventUpdatePlayerInventory eventUpdatePlayerInventory)
            => CloseThis();

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdatePlayerInventory>(OnEventUpdateInventory);
            EventBroker.Instance().UnsubscribeMessage<EventHideUpdateWindows>(OnHideMenu);
        }

        void ShowItemToBuy(CombinedWearables reward) {
            var instance = Instantiate(reward, rectTransform);
            instance.GetComponent<RectTransform>().localPosition = Vector3.zero;
            foreach (var canvasGroup in instance.canvasGroups) {
                canvasGroup.alpha = 1;
            }

            for (var i = 1; i < instance.transform.childCount; i++) {
                instance.transform.GetChild(i).gameObject.SetActive(false);
            }

            Resize(instance, sizeToDisplayReward);
            instance.GetComponent<AssignCombinedWearableToUpCycle>().enabled = false;
        }

        void Resize(CombinedWearables go, float newScale) {
            var newSize = new Vector2(newScale, newScale);
            go.GetComponent<RectTransform>().localScale = newSize;
        }
    }
}