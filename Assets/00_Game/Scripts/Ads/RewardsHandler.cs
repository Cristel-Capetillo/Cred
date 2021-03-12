using Cred.Scripts;
using EventBrokerFolder;
using UnityEngine;

namespace Cred._00_Game.Scripts.Ads {
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