using Currency.Coins;
using UnityEngine;
using Utilities;

namespace Ads {
    public class RewardsHandler : MonoBehaviour {
        [SerializeField] int coinsToRewardOnSuccessfulAdWatch;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventAdWatched>(RewardAdWatched);
        }

        void RewardAdWatched(EventAdWatched eventAdWatched) {
            if(eventAdWatched.doubleMissionRewards) {
                EventBroker.Instance().SendMessage(new EventUpdateCoins(coinsToRewardOnSuccessfulAdWatch * 2));
            }
            else {
                EventBroker.Instance().SendMessage(new EventUpdateCoins(coinsToRewardOnSuccessfulAdWatch));
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(RewardAdWatched);
        }
    }
}