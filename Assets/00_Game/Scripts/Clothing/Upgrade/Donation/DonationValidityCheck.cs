using Clothing.Inventory;
using Currency.Coins;
using HUD.MysteryBox;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class DonationValidityCheck : MonoBehaviour {
        DonationPopUpWarnings donationPopUpWarnings;
        CoinsSpend coinsSpend;
        Coin coin;
        
        public RewardDisplay rewardDisplay;
        public Image itemToDonateSlot;
        public Image upgradedItemSlot;
        public Image stylePointsBackground;
        
        public Button confirmButton;
        public Button[] alternativesButtons;
        int upgradedOriginalStylePoints;
        int costOfDonation;
        int upgradedStylePoints;
        CanvasGroup canvasGroup;

        CombinedWearables originalWearable;
        CombinedWearables upgradedWearable;
        
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
            EventBroker.Instance().SubscribeMessage<EventCoinsToSpend>(UpdateStylePoints);
            EventBroker.Instance().SubscribeMessage<EventTogglePopWindow>(OnClosePopUpWindow);
            EventBroker.Instance().SubscribeMessage<EventHideUpdateWindows>(HideWindow);
        }
        
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
            EventBroker.Instance().UnsubscribeMessage<EventCoinsToSpend>(UpdateStylePoints);
            EventBroker.Instance().SubscribeMessage<EventHideUpdateWindows>(HideWindow);
        }
        
        void OnClosePopUpWindow(EventTogglePopWindow obj) {
            if(rewardDisplay != null) {
                rewardDisplay.gameObject.SetActive(obj.popWindowIsActive);
            }
            if(!obj.popWindowIsActive)
                TryRemoveChildren();
        }

        void HideWindow(EventHideUpdateWindows window) {
            if (window.shouldHide) {
                DeactivateWindow();
            }
        }
        
        void Start() {
            donationPopUpWarnings = GetComponent<DonationPopUpWarnings>();
            canvasGroup = GetComponent<CanvasGroup>();
            coinsSpend = FindObjectOfType<CoinsSpend>();
            coin = FindObjectOfType<Coin>();
            foreach (var button in alternativesButtons) {
                button.interactable = false;
            }
        }

        void UpdateStylePoints(EventCoinsToSpend eventCoinsToSpend) {
            costOfDonation = eventCoinsToSpend.coins;
            upgradedStylePoints = upgradedWearable.stylePoints;
            upgradedWearable.stylePoints = eventCoinsToSpend.stylePoints + upgradedOriginalStylePoints;
            upgradedWearable.GetComponent<IconUpdate>().UpdateInformation();
        }

        public void DoesItemQualifyForDonation(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            if (!ValidateItem(eventAddToUpgradeSlot.combinedWearable)) {
                donationPopUpWarnings.ShowWarningPopUp(eventAddToUpgradeSlot.combinedWearable);
                return;
            }
            
            donationPopUpWarnings.DisableWarning();
            TryRemoveChildren();
            
            originalWearable = Instantiate(eventAddToUpgradeSlot.combinedWearable, itemToDonateSlot.transform, true);
            originalWearable.Amount = eventAddToUpgradeSlot.combinedWearable.Amount;
            originalWearable.stylePoints = eventAddToUpgradeSlot.combinedWearable.stylePoints;
            originalWearable.GetComponent<IconUpdate>().UpdateInformation();
            var scale = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            originalWearable.transform.localPosition = Vector2.zero;
            originalWearable.GetComponent<RectTransform>().localScale = scale;
            Destroy(originalWearable.GetComponent<Button>());
            originalWearable.GetComponent<CanvasGroup>().blocksRaycasts = false;

            upgradedWearable = Instantiate(eventAddToUpgradeSlot.combinedWearable, upgradedItemSlot.transform, true);
            upgradedWearable.Amount = 1;
            upgradedWearable.stylePoints = eventAddToUpgradeSlot.combinedWearable.stylePoints;
            upgradedOriginalStylePoints = upgradedWearable.stylePoints;
            upgradedWearable.GetComponent<IconUpdate>().UpdateInformation();
            var scale2 = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            upgradedWearable.transform.localPosition = Vector2.zero;
            upgradedWearable.GetComponent<RectTransform>().localScale = scale2;
            upgradedWearable.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Destroy(upgradedItemSlot.GetComponent<Button>());

            EventBroker.Instance().SendMessage(new EventUpdateAlternativesButtons());
        }
        
        void TryRemoveChildren() {
            if (itemToDonateSlot.transform.childCount > 0) {
                Destroy(itemToDonateSlot.transform.GetChild(0).gameObject);
            }
            if (upgradedItemSlot.transform.childCount > 0) {
                Destroy(upgradedItemSlot.transform.GetChild(0).gameObject);
            }
        }

        public bool ValidateItem(CombinedWearables combinedWearables) {
            return combinedWearables.stylePoints < combinedWearables.rarity.MaxValue && 
                   combinedWearables.Amount > 1;
        }
        
        public bool CheckForMaxStylePoints(int buttonStylePoints) {
            if (originalWearable == null) return false;
            return buttonStylePoints + originalWearable.stylePoints <= originalWearable.rarity.MaxValue;
        }

        public void OnConfirm() {
            coin.Coins -= costOfDonation;
            GenerateNewItem();
            DeactivateWindow();
            Debug.Log(coinsSpend.equalSignText.gameObject.activeSelf);
        }
        
        void GenerateNewItem() {
            var instance = Instantiate(upgradedWearable);
            instance.isPredefined = false;
            instance.GetComponent<IconUpdate>().UpdateImages();
            instance.GetComponent<IconUpdate>().UpdateInformation();
            stylePointsBackground.gameObject.SetActive(true);

            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(originalWearable, -2));
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(instance, 1));
            EventBroker.Instance().SendMessage(new EventShowReward(instance,upgradedWearable.stylePoints - upgradedOriginalStylePoints));
            EventBroker.Instance().SendMessage(new EventUpdateWearableHud());
            
            Destroy(instance.gameObject);
        }

        void DeactivateWindow() {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            confirmButton.interactable = false;
            coinsSpend.equalSignText.gameObject.SetActive(false);

            EventBroker.Instance().SendMessage(new EventTogglePopWindow(false));
            Debug.Log(gameObject.name);
            if (itemToDonateSlot.transform.childCount > 0) {
                Destroy(itemToDonateSlot.transform.GetChild(0).gameObject);
            }
            if (upgradedItemSlot.transform.childCount > 0) {
                Destroy(upgradedItemSlot.transform.GetChild(0).gameObject);
            }
        }
        
        public void CloseWindow() {
            DeactivateWindow();
        }
    }
}
