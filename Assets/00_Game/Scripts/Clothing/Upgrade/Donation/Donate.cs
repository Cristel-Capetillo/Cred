using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class Donate : MonoBehaviour {
        CombinedWearables combinedWearables;
        CoinsSpend coinsSpend;
        Coin coin;
        Button button;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
            coinsSpend = FindObjectOfType<CoinsSpend>();
            coin = FindObjectOfType<Coin>();
            button = GetComponent<Button>();
        }

        void AssignedWearable(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            combinedWearables = eventAddToUpgradeSlot.combinedWearable;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
            
        }
    }
}