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

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
            donationPopUpWarnings = GetComponent<DonationPopUpWarnings>();
            foreach (var button in alternativesButtons) {
                button.interactable = false;
            }
        }

        public void DoesItemQualifyForDonation(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            Debug.Log(eventAddToUpgradeSlot.combinedWearable.Amount);
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
            
            var instance2 = Instantiate(eventAddToUpgradeSlot.combinedWearable, upgradedItemSlot.transform, true);
            var scale2 = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            instance2.transform.localPosition = Vector2.zero;
            instance2.GetComponent<RectTransform>().localScale = scale2;
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
        }
    }
}
