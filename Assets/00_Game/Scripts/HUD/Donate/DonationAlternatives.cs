using System;
using System.Collections.Generic;
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
        public Image slots;
        int[] donatedCoins = { -1000, -2000, -3000 };
        PlayerInventory playerInventory;
        DonationHandler donationHandler;

        void Start() {
            playerInventory = FindObjectOfType<PlayerInventory>();
            donationHandler = FindObjectOfType<DonationHandler>();
            foreach (var button in donationHandler.alternativesButtons) {
                button.interactable = false;
            }
        }

        void OnEnable() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
        }

        void OnDisable() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignUpCycleSlot);
        }

        public void AssignUpCycleSlot(EventAddToUpgradeSlot eventAddUpCycleClothes) {
            AssignToSlot(eventAddUpCycleClothes.combinedWearable);
        }
        
        void AssignToSlot(CombinedWearables combinedWearables) {
            donationHandler.qualifiesForDonation();
            CheckIfValidForDonation(combinedWearables);
            Debug.Log(donationHandler.isGood);
            var instance = Instantiate(combinedWearables, slots.transform, true);
                var scale = combinedWearables.GetComponent<RectTransform>().localScale;
                instance.transform.localPosition = Vector2.zero;
                instance.GetComponent<RectTransform>().localScale = scale;
                Destroy(instance.GetComponent<AssignCombinedWearableToUpCycle>());
                Destroy(instance.GetComponent<Button>());
        }
        
        void CheckIfValidForDonation(CombinedWearables combinedWearables) {
            if (donationHandler.isGood) {
                for (var buttonToBeActive = 0; buttonToBeActive < donationHandler.stylePointsToUpgrade; buttonToBeActive++) {
                    foreach (var button in donationHandler.alternativesButtons) {
                        button.interactable = true;
                    }
                }
            }
            if (!(playerInventory.Amount(PlayerInventory.GetName(combinedWearables)) >= 2)) {
                donationHandler.warningText.text = "This item does not have any duplicate yet. Come back later!";
                Debug.Log("This item does not have any duplicate yet. Come back later!");
            }
            else if (!(donationHandler.CheckIfMaxStylePointsReached(combinedWearables.stylePoints, combinedWearables.rarity) >= 1)) {
                donationHandler.warningText.text = "This item already has its maximum style points";
                Debug.Log("This item already has its maximum style points");
            }
            else {
                Debug.Log("Something went wrong..");
            }
        }
        
        public void DonateAlternative1() {
            donationHandler.DonateMeBaby(combinedWearables, 1);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[0]));
        }

        public void DonateAlternative2() {
            donationHandler.DonateMeBaby(combinedWearables, 2);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[1]));
        }

        public void DonateAlternative3() {
            donationHandler.DonateMeBaby(combinedWearables, 3);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[2]));
        }
    }
}