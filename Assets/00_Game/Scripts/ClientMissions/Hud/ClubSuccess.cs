using ClientMissions.Messages;
using Currency.Coins;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud {
    public class ClubSuccess : MonoBehaviour {
        [SerializeField] Text rewardText;
        [SerializeField] Text followersRewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        int currencyReward;
        int followersReward;
        void Start() {
            EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(ShowReward);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(ShowReward);
        }

        void ShowReward(ShowRewardMessage rewardMessage) {
            GetComponent<Canvas>().enabled = true;
            currencyReward = rewardMessage.CurrencyReward;
            followersReward = rewardMessage.FollowersReward;
            //TODO: Currency and Followers!
            rewardText.text = rewardMessage.CurrencyReward.ToString();
            followersRewardText.text = rewardMessage.FollowersReward.ToString();
        }
        public void CollectReward() {
            EventBroker.Instance().SendMessage(new EventUpdateCoins(currencyReward));
            EventBroker.Instance().SendMessage(new UpdateFollowersMessage(followersReward));
            mainMenuButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
        }
    }
}
