using Clothing;
using Clothing.Inventory;
using Clothing.Upgrade;
using Clothing.Upgrade.UpCycle;
using Currency.Coins;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Donate {
    public class DonationAlternatives : MonoBehaviour{
        CombinedWearables combinedWearables;
        public Image itemToDonateSlot;
        int[] donatedCoins = { -1000, -2000, -3000 };
        PlayerInventory playerInventory;
        DonationValidityCheck donationValidityCheck;
        

        void Start() {
            playerInventory = FindObjectOfType<PlayerInventory>();
            donationValidityCheck = FindObjectOfType<DonationValidityCheck>();
            donationValidityCheck.warningPopUp.SetActive(false);
            foreach (var button in donationValidityCheck.alternativesButtons) {
                button.interactable = false;
            }
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DisplayDonationItemOnSlot);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(DisplayDonationItemOnSlot);
        }

        public void DisplayDonationItemOnSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignDonationItemToSlot(eventAddUpCycleClothes.combinedWearable);
        }
        
        void AssignDonationItemToSlot(CombinedWearables combinedWearables) {
            donationValidityCheck.ItemQualifiesForDonation(combinedWearables);
            if (ProceedWithDonation(combinedWearables)) {
                var instance = Instantiate(combinedWearables, itemToDonateSlot.transform, true);
                var scale = combinedWearables.GetComponent<RectTransform>().localScale;
                instance.transform.localPosition = Vector2.zero;
                instance.GetComponent<RectTransform>().localScale = scale;
                Destroy(instance.GetComponent<AssignCombinedWearableToUpCycle>());
                Destroy(instance.GetComponent<Button>());
            }
        }
        
        bool ProceedWithDonation(CombinedWearables combinedWearables) {
            if (donationValidityCheck.canBeDonated) {
                for (var buttonToBeActive = 0; buttonToBeActive < donationValidityCheck.stylePointsToUpgrade; buttonToBeActive++) {
                    foreach (var button in donationValidityCheck.alternativesButtons) {
                        button.interactable = true;
                    }
                }
            }
            if (!(playerInventory.Amount(PlayerInventory.GetName(combinedWearables)) >= 2)) {
                ToggleNoDuplicatesWarning();
            }
            else if (!(donationValidityCheck.MaxStylePointsCheck(combinedWearables.stylePoints, combinedWearables.rarity) >= 1)) {
                ToggleMaxStylePointsWarning();
            }
            else {
                Debug.Log("Something went wrong..");
            }
            return false;
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
        
        public void DonateAlternative1() {
            donationValidityCheck.GetDonationUpgrade(combinedWearables, 1);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[0]));
        }

        public void DonateAlternative2() {
            donationValidityCheck.GetDonationUpgrade(combinedWearables, 2);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[1]));
        }

        public void DonateAlternative3() {
            donationValidityCheck.GetDonationUpgrade(combinedWearables, 3);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[2]));
        }
    }
}