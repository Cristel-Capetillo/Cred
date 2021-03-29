using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class Donate : MonoBehaviour {
        CombinedWearables combinedWearables;
        int coins;
        int stylePoints;
        Coin coin;
        Button button;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
            EventBroker.Instance().SubscribeMessage<EventCoinsToSpend>(AssignedCoins);
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();

        }

        void AssignedWearable(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            combinedWearables = eventAddToUpgradeSlot.combinedWearable;
            ActivateButton();
        }

        void AssignedCoins(EventCoinsToSpend eventCoinsToSpend) {
            coins = eventCoinsToSpend.coins;
            stylePoints = eventCoinsToSpend.stylePoints;
            ActivateButton();
        }

        public void ConfirmDonation() {
            coin.Coins -= coins;
            combinedWearables.stylePoints += stylePoints;
        }

        void ActivateButton() {
            button.interactable = coin.Coins > 0 && combinedWearables != null;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
            EventBroker.Instance().UnsubscribeMessage<EventCoinsToSpend>(AssignedCoins);
        }
    }
}