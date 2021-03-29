using ClientMissions.Messages;
using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud {
    public class ClubSuccess : MonoBehaviour {
        [SerializeField] Text rewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        int currentPoints;
        void Start() {
            EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(ShowReward);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(ShowReward);
        }

        void ShowReward(ShowRewardMessage rewardMessage) {
            GetComponent<Canvas>().enabled = true;
            currentPoints = rewardMessage.RewardPoints;
            rewardText.text = "Reward: " + rewardMessage.RewardPoints;
        }
        //TODO: Separate View and controller logic!
        public void CollectReward() {
            EventBroker.Instance().SendMessage(new EventUpdateCoins(currentPoints));
            mainMenuButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
        }
    }
}
