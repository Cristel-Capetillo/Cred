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
                FindObjectOfType<Coin>().Coins += coinsToRewardOnSuccessfulAdWatch * 2;
            }
            else {
                FindObjectOfType<Coin>().Coins += coinsToRewardOnSuccessfulAdWatch;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventAdWatched>(RewardAdWatched);
        }
    }
}