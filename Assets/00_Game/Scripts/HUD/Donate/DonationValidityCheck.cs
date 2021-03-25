using Clothing;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace HUD.Donate {
    public class DonationValidityCheck : MonoBehaviour {
        PlayerInventory playerInventory;
        [HideInInspector] public int stylePointsToUpgrade;
        int addedStylePoints;
        public bool canBeDonated;
        
        public Button[] alternativesButtons;
        public Text warningText;
        public GameObject warningPopUp;

        void Start() {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
        public void GetDonationUpgrade(CombinedWearables wearable, int stylePointsToAdd) {
            addedStylePoints = stylePointsToAdd;
            //EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, -1));
            wearable.stylePoints += addedStylePoints;
            //EventBroker.Instance().SendMessage(new EventUpdatePlayerInventory(combinedWearables, 1));
        }
        
        public void ItemQualifiesForDonation(CombinedWearables combinedWearables) {
            if (MaxStylePointsCheck(combinedWearables.stylePoints, combinedWearables.rarity) >= 1 && 
                playerInventory.Amount(PlayerInventory.GetName(combinedWearables)) >= 2) {
                canBeDonated = true;
            }
            else {
                Debug.Log("Cannot donate man!");
                canBeDonated = false;
            }
        }
        
        public int MaxStylePointsCheck(int currentPoints, Rarity rarity) {
            stylePointsToUpgrade = rarity.MaxValue - currentPoints;
            return stylePointsToUpgrade;
        }
    }
}
