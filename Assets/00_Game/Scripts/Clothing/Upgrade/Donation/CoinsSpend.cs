using Currency.Coins;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class CoinsSpend : MonoBehaviour, IPointerClickHandler {
        public int coinsToSpend;
        public int stylePointsReward;
        DonationValidityCheck donationValidityCheck;
        CombinedWearables combinedWearables;
        Coin coin;
        Button button;
        public Button confirmButton;

        void Start() {
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();
            donationValidityCheck = FindObjectOfType<DonationValidityCheck>();
            confirmButton.interactable = false;
            EventBroker.Instance().SubscribeMessage<EventUpdateAlternativesButtons>(OnButtonsInteractable);
            EventBroker.Instance().SubscribeMessage<EventTogglePopWindow>(OnClosePopUpWindow);
        }
        
        void OnClosePopUpWindow(EventTogglePopWindow obj) {
            print("Started!");
            if (!obj.popWindowIsActive)
                button.interactable = false;
            print("Success!");
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateAlternativesButtons>(OnButtonsInteractable);
            EventBroker.Instance().UnsubscribeMessage<EventTogglePopWindow>(OnClosePopUpWindow);
        }
        

        void OnButtonsInteractable(EventUpdateAlternativesButtons eventUpdateAlternativesButtons) {
            button.interactable = coin.Coins >= coinsToSpend && donationValidityCheck.CheckForMaxStylePoints(stylePointsReward);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (coin.Coins >= coinsToSpend && button.interactable) {
                confirmButton.interactable = true;
                EventBroker.Instance().SendMessage(new EventCoinsToSpend(coinsToSpend, stylePointsReward));
            }
        }
    }
}