using Currency.Coins;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.ClubMissions {
    public class ClubSuccessMessage : MonoBehaviour {
        [SerializeField] Text rewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        int currentPoints;
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventShowReward>(ShowReward);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventShowReward>(ShowReward);
        }

        void ShowReward(EventShowReward reward) {
            GetComponent<Canvas>().enabled = true;
            currentPoints = reward.rewardPoints;
            rewardText.text = "Reward: " + reward.rewardPoints;
        }

        public void CollectReward() {
            EventBroker.Instance().SendMessage(new EventUpdateCoins(currentPoints));
            mainMenuButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
        }
    }
}
