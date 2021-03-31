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
        public Text equalSignText;

        void Start() {
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();
            donationValidityCheck = FindObjectOfType<DonationValidityCheck>();
            confirmButton.interactable = false;
            EventBroker.Instance().SubscribeMessage<EventUpdateAlternativesButtons>(OnButtonsInteractable);
            EventBroker.Instance().SubscribeMessage<EventTogglePopWindow>(OnClosePopUpWindow);
        }
        
        void OnClosePopUpWindow(EventTogglePopWindow obj) {
            if (!obj.popWindowIsActive) {
                button.interactable = false;
                equalSignText.gameObject.SetActive(false);
            }
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
                equalSignText.gameObject.SetActive(true);
                confirmButton.interactable = true;
                EventBroker.Instance().SendMessage(new EventCoinsToSpend(coinsToSpend, stylePointsReward));
            }
        }
    }
}