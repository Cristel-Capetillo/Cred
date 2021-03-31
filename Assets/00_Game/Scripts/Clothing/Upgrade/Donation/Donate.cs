using UnityEngine;
using Utilities;

namespace Clothing.Upgrade.Donation {
    public class Donate : MonoBehaviour {
        CombinedWearables combinedWearables;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
        }

        void AssignedWearable(EventAddToUpgradeSlot eventAddToUpgradeSlot) {
            combinedWearables = eventAddToUpgradeSlot.combinedWearable;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAddToUpgradeSlot>(AssignedWearable);
            
        }
    }
}