using Clothing;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Donate {
    public class DonationHandler : MonoBehaviour {
        CombinedWearables combinedWearables;
        PlayerInventory playerInventory;
        [HideInInspector]
        public int stylePointsToUpgrade;
        int addedStylePoints;
        public bool isGood;
        
        public Button[] alternativesButtons;
        public Text warningText;

        void Start() {
            combinedWearables = GetComponent<CombinedWearables>();
            playerInventory = GetComponent<PlayerInventory>();
        }
        public void DonateMeBaby(CombinedWearables wearable, int stylePointsToAdd) {
            addedStylePoints = stylePointsToAdd;
            combinedWearables = wearable;
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, -1));
            combinedWearables.stylePoints += addedStylePoints;
            EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, 1));
        }
        public void qualifiesForDonation() {
            if (CheckIfMaxStylePointsReached(combinedWearables.stylePoints, combinedWearables.rarity) >= 1 && playerInventory.Amount(PlayerInventory.GetName(combinedWearables)) >= 2) {
                isGood = true;
            }
            else {
                Debug.Log("nothing man");
                isGood = false;
            }
        }
        
        public int CheckIfMaxStylePointsReached(int currentPoints, Rarity rarity) {
            stylePointsToUpgrade = rarity.MaxValue - currentPoints;
            return stylePointsToUpgrade;
        }
    }
}
