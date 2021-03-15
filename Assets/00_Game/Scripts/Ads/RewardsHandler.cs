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
            FindObjectOfType<Coin>().Coins += coinsToRewardOnSuccessfulAdWatch;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(RewardAdWatched);
        }
    }
}