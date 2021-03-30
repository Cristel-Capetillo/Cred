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
        Coin coin;
        Button button;
        public Button confirmButton;

        void Start() {
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();
            confirmButton.interactable = false;
        }
        void Update() {
            button.interactable = coin.Coins >= coinsToSpend;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (coin.Coins >= coinsToSpend) {
                confirmButton.interactable = true;
                EventBroker.Instance().SendMessage(new EventCoinsToSpend(coinsToSpend, stylePointsReward));
            }
        }
    }
}