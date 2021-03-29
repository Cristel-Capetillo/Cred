using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class DonationValidityCheck : MonoBehaviour {
        PlayerInventory playerInventory;
        [HideInInspector] public int stylePointsToUpgrade;
        int addedStylePoints;
        public Image itemToDonateSlot;
        DonationPopUpWarnings donationPopUpWarnings;
        public Button[] alternativesButtons;
        public Text warningText;
        public GameObject warningPopUp;

        void Start() {
            playerInventory = FindObjectOfType<PlayerInventory>();
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DoesItemQualifyForDonation);
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
            if (itemToDonateSlot.transform.childCount > 0) {
                Destroy(itemToDonateSlot.transform.GetChild(0).gameObject);
            }
            var instance = Instantiate(eventAddToUpgradeSlot.combinedWearable, itemToDonateSlot.transform, true);
            var scale = itemToDonateSlot.GetComponent<RectTransform>().localScale;
            instance.transform.localPosition = Vector2.zero;
            instance.GetComponent<RectTransform>().localScale = scale;
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
                EventBroker.Instance().SendMessage(new EventFinishedLoadingPlayerInventory());
            }
        }
    }
}
