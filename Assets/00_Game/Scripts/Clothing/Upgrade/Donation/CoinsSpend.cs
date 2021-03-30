using System;
using Currency.Coins;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class CoinsSpend : MonoBehaviour, IPointerClickHandler {
        [SerializeField] int coinsToSpend;
        public int stylePointsReward;
        DonationValidityCheck donationValidityCheck;
        Coin coin;
        Button button;

        void Start() {
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();
            EventBroker.Instance().SubscribeMessage<EventCoinsDropDown>(ValidateCoins);
        }

        void Update() {
            if (coin.Coins >= coinsToSpend) {
                button.interactable = true;
            }
        }

        void ValidateCoins(EventCoinsDropDown eventCoinsDropDown) {
            button.interactable = coin.Coins > coinsToSpend;
        }

        public void OnPointerClick(PointerEventData eventData) {
            EventBroker.Instance().SendMessage(new EventCoinsToSpend(coinsToSpend, stylePointsReward));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventCoinsDropDown>(ValidateCoins);
        }
    }
}