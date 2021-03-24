using Clothing;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Donate {
    public class DonationHandler : MonoBehaviour {
        CombinedWearables combinedWearables;
        PlayerInventory playerInventory;
        int stylePointsToUpgrade;
        int addedStylePoints;
        
        public Button[] alternativesButtons;
        public Text warningText;

        void Start() {
            combinedWearables = GetComponent<CombinedWearables>();
            playerInventory = FindObjectOfType<PlayerInventory>();
            foreach (var button in alternativesButtons) {
                button.interactable = false;
            }
        }

        void CheckIfValidForDonation() {
            if (qualifiesForDonation) {
                for (var buttonToBeActive = 0; buttonToBeActive < stylePointsToUpgrade; buttonToBeActive++) {
                    foreach (var button in alternativesButtons) {
                        button.interactable = true;
                    }
                }
            }
            if (!(playerInventory.combineWearablesAmount[combinedWearables] >= 2)) {
                warningText.text = "This item does not have any duplicate yet. Come back later!";
            }
            else if (!(CheckIfMaxStylePointsReached(combinedWearables.stylePoints, combinedWearables.rarity) >= 1)) {
                warningText.text = "This item already has its maximum style points";
            }
        }

        public void DonateMeBaby(CombinedWearables wearable, int stylePointsToAdd) {
            addedStylePoints = stylePointsToAdd;
            combinedWearables = wearable;
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, -1));
            combinedWearables.stylePoints += addedStylePoints;
            // Will need to be remade (only returns the old item with old stylepoints instead of the one with increased amount) 
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, 1));
        }

        bool qualifiesForDonation => CheckIfMaxStylePointsReached(combinedWearables.stylePoints, combinedWearables.rarity) >= 1 
                       && playerInventory.combineWearablesAmount[combinedWearables] >= 2;
        
        int CheckIfMaxStylePointsReached(int currentPoints, Rarity rarity) {
            stylePointsToUpgrade = rarity.MaxValue - currentPoints;
            return stylePointsToUpgrade;
        }
    }
}
