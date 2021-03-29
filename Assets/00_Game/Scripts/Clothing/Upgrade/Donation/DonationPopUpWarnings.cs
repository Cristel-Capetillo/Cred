using UnityEngine;

namespace Clothing.Upgrade.Donation {
    public class DonationPopUpWarnings : MonoBehaviour{
        CombinedWearables combinedWearables;
        DonationValidityCheck donationValidityCheck;

        public void ShowWarningPopUp(CombinedWearables combinedWearables) {
            if (combinedWearables.Amount < 2) {
                ToggleNoDuplicatesWarning();
            }
            if (combinedWearables.stylePoints >= combinedWearables.rarity.MaxValue) {
                ToggleMaxStylePointsWarning();
            }
        }

        void ToggleNoDuplicatesWarning() {
            donationValidityCheck.warningPopUp.SetActive(true);
            donationValidityCheck.warningText.text = "This item does not have any duplicate yet. Come back later!";
            Debug.Log("This item does not have any duplicate yet. Come back later!");
        }

        void ToggleMaxStylePointsWarning() {
            donationValidityCheck.warningPopUp.SetActive(true);
            donationValidityCheck.warningText.text = "This item already has its maximum style points";
            Debug.Log("This item already has its maximum style points");
        }
    }
}