using UnityEngine;
using UnityEngine.EventSystems;

namespace Clothing.Upgrade.Donation {
    public class ClearDonationSlot : MonoBehaviour {
        DonationValidityCheck donationValidityCheck;

        public void ClearSlot() {
            donationValidityCheck = FindObjectOfType<DonationValidityCheck>();
            var go = EventSystem.current.currentSelectedGameObject;
            if (go.transform.childCount <= 0) return;
            Destroy(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            Destroy(donationValidityCheck.upgradedItemSlot.transform.GetChild(0).gameObject);
        }
    }
}