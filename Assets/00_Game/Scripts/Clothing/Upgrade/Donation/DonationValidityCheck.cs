using System;
using Clothing.Inventory;
using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class DonationValidityCheck : MonoBehaviour {
        public Image itemToDonateSlot;
        public Image upgradedItemSlot;
        DonationPopUpWarnings donationPopUpWarnings;
        public Button[] alternativesButtons;
        Coin coin;
        int upgradedOriginalStylePoints;

        CombinedWearables originalWearable;
        CombinedWearables UpgradedWearable;
        
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
            EventBroker.Instance().SubscribeMessage<EventCoinsToSpend>(UpdateStylePoints);
        }
        void Start() {
            donationPopUpWarnings = GetComponent<DonationPopUpWarnings>();
            coin = FindObjectOfType<Coin>();
            foreach (var button in alternativesButtons) {
                button.interactable = false;
            }
        }

        void UpdateStylePoints(EventCoinsToSpend eventCoinsToSpend) {
            UpgradedWearable.stylePoints = eventCoinsToSpend.stylePoints + upgradedOriginalStylePoints;
            UpgradedWearable.GetComponent<IconUpdate>().UpdateInformation();
        }

        public void DoesItemQualifyForDonation(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            if (!ValidateItem(eventAddToUpgradeSlot.combinedWearable)) {
                donationPopUpWarnings.ShowWarningPopUp(eventAddToUpgradeSlot.combinedWearable);
                return;
            }
            if (itemToDonateSlot.transform.childCount > 0 ) {
                Destroy(itemToDonateSlot.transform.GetChild(0).gameObject);
            }
            if (upgradedItemSlot.transform.childCount > 0 ) {
                Destroy(upgradedItemSlot.transform.GetChild(0).gameObject);
            }

            var instance = Instantiate(eventAddToUpgradeSlot.combinedWearable, itemToDonateSlot.transform, true);
            instance.Amount = eventAddToUpgradeSlot.combinedWearable.Amount;
            instance.stylePoints = eventAddToUpgradeSlot.combinedWearable.stylePoints;
            instance.GetComponent<IconUpdate>().UpdateInformation();
            var scale = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            instance.transform.localPosition = Vector2.zero;
            instance.GetComponent<RectTransform>().localScale = scale;
            Destroy(instance.GetComponent<Button>());
            
            UpgradedWearable = Instantiate(eventAddToUpgradeSlot.combinedWearable, upgradedItemSlot.transform, true);
            UpgradedWearable.Amount = eventAddToUpgradeSlot.combinedWearable.Amount;
            UpgradedWearable.stylePoints = eventAddToUpgradeSlot.combinedWearable.stylePoints;
            upgradedOriginalStylePoints = UpgradedWearable.stylePoints;
            UpgradedWearable.GetComponent<IconUpdate>().UpdateInformation();
            var scale2 = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            UpgradedWearable.transform.localPosition = Vector2.zero;
            UpgradedWearable.GetComponent<RectTransform>().localScale = scale2;
            Destroy(instance.GetComponent<Button>());
        }

        public bool ValidateItem(CombinedWearables combinedWearables) {
            return combinedWearables.stylePoints < combinedWearables.rarity.MaxValue && 
                   combinedWearables.Amount > 1;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                foreach (var test in FindObjectsOfType<CombinedWearables>()) {
                    test.Amount++;
                }
                EventBroker.Instance().SendMessage(new EventUpdateWearableHud());
            }

            if (Input.GetKeyDown(KeyCode.G)) {
                coin.Coins += 1000;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
            EventBroker.Instance().UnsubscribeMessage<EventCoinsToSpend>(UpdateStylePoints);
        }
    }
}
