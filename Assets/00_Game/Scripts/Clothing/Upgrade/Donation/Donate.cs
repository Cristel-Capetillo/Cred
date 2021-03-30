using Currency.Coins;
using HUD.Clothing;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class Donate : MonoBehaviour {
        CombinedWearables combinedWearables;
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
            ActivateButton();
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