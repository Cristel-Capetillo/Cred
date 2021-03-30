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

        CombinedWearables originalWearable;
        CombinedWearables UpgradedWearable;
        
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdateStylePoints>(UpdateStylePoints);
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
        }
        void Start() {
            donationPopUpWarnings = GetComponent<DonationPopUpWarnings>();
            coin = FindObjectOfType<Coin>();
            foreach (var button in alternativesButtons) {
                button.interactable = false;
            }
        }

        void UpdateStylePoints(EventUpdateStylePoints eventUpdateStylePoints) {
            UpgradedWearable.stylePoints += eventUpdateStylePoints.stylePoints;
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
            var scale = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            instance.transform.localPosition = Vector2.zero;
            instance.GetComponent<RectTransform>().localScale = scale;
            Destroy(instance.GetComponent<Button>());
            
            UpgradedWearable = Instantiate(eventAddToUpgradeSlot.combinedWearable, upgradedItemSlot.transform, true);
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
            EventBroker.Instance().UnsubscribeMessage<EventUpdateStylePoints>(UpdateStylePoints);
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);        }
    }
}
