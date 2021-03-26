using Clothing;
using Clothing.Inventory;
using Clothing.Upgrade;
using Currency.Coins;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Donate {
    public class DonationAlternatives : MonoBehaviour{
        CombinedWearables combinedWearables;
        public Image itemToDonateSlot;
        public Image upgradedClothingSlot;
        public Text upgradedStylepointsText;
        PlayerInventory playerInventory;
        DonationValidityCheck donationValidityCheck;
        int coinDonation;
        int stylePointsToGive;
        
        void Start() {
            playerInventory = FindObjectOfType<PlayerInventory>();
            donationValidityCheck = FindObjectOfType<DonationValidityCheck>();
            donationValidityCheck.warningPopUp.SetActive(false);
            upgradedStylepointsText.enabled = false;
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(DisplayDonationItemOnSlot);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(DisplayDonationItemOnSlot);
        }

        void DisplayDonationItemOnSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignDonationItemToSlot(eventAddUpCycleClothes.combinedWearable);
        }

        void AssignDonationItemToSlot(CombinedWearables combinedWearables) {
            donationValidityCheck.ItemQualifiesForDonation(combinedWearables);
            ProceedWithDonation(combinedWearables);
            var instance = Instantiate(combinedWearables, itemToDonateSlot.transform, true);
            var scale = combinedWearables.GetComponent<RectTransform>().localScale;
            instance.transform.localPosition = Vector2.zero;
            instance.GetComponent<RectTransform>().localScale = scale;
            Destroy(instance.GetComponent<AssignCombinedWearableToUpCycle>());
            Destroy(instance.GetComponent<Button>());
        }
        
        void ProceedWithDonation(CombinedWearables combinedWearables) {
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
            if (!(donationValidityCheck.MaxStylePointsCheck(combinedWearables.stylePoints, combinedWearables.rarity) >= 1)) {
                ToggleMaxStylePointsWarning();
            }
            else {
                Debug.Log("Something went wrong..");
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

        void WhichAlternativeDidPlayerChoose(int coins, int stylepoints) {
            coinDonation = coins;
            stylePointsToGive = stylepoints;
            donationValidityCheck.GetDonationUpgrade(combinedWearables, stylepoints);
        }

        public void SuccessfulDonation(CombinedWearables combinedWearables) {
            // Set Donation popUpWindow to inactive
            EventBroker.Instance().SendMessage(new EventUpdateCoins(coinDonation));
            combinedWearables.stylePoints += stylePointsToGive;
            Instantiate(combinedWearables, upgradedClothingSlot.transform, true);
            upgradedStylepointsText.text = stylePointsToGive.ToString();
            upgradedStylepointsText.enabled = true;
        }
        
        public void DonateAlternative1() {
            coinDonation = -1000;
            stylePointsToGive = 1;
            WhichAlternativeDidPlayerChoose(coinDonation, stylePointsToGive);
        }

        public void DonateAlternative2() {
            coinDonation = -2000;
            stylePointsToGive = 2;
            WhichAlternativeDidPlayerChoose(coinDonation, stylePointsToGive);
        }

        public void DonateAlternative3() {
            coinDonation = -3000;
            stylePointsToGive = 3;
            WhichAlternativeDidPlayerChoose(coinDonation, stylePointsToGive);
        }
    }
}