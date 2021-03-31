using System;
using HUD.MysteryBox;
using UnityEngine;
using UnityEngine.UI;

namespace Clothing.Upgrade.Donation {
    public class DonationPopUpWarnings : MonoBehaviour{
        CombinedWearables combinedWearables;
        DonationValidityCheck donationValidityCheck;
        public GameObject warningPopUp;
        public Text warningText;
        public void ShowWarningPopUp(CombinedWearables combinedWearables) {
            if (combinedWearables.Amount < 2) {
                ToggleNoDuplicatesWarning();
            }
            if (combinedWearables.stylePoints >= combinedWearables.rarity.MaxValue) {
                ToggleMaxStylePointsWarning();
            }
        }

        public void DisableWarning() {
            if (warningPopUp.activeSelf) {
                warningPopUp.SetActive(false);
            } 
        }

        void ToggleNoDuplicatesWarning() {
            warningPopUp.SetActive(true);
            warningText.text = "This item does not have any duplicate yet. Come back later!";
            Debug.Log("This item does not have any duplicate yet. Come back later!");
        }

        void ToggleMaxStylePointsWarning() {
            warningPopUp.SetActive(true);
            warningText.text = "This item already has its maximum style points";
            Debug.Log("This item already has its maximum style points");
        }
    }
}