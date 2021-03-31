using System.Collections;
using ClientMissions.Messages;
using Clothing.DressUp;
using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions.Hud {
    public class ClubSuccess : MonoBehaviour {
        [SerializeField] Text rewardText;
        [SerializeField] Text followersRewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        [SerializeField, Header("Set the amount of coins to be displayed when pressing collect button")]
        float animatedResourceDivider = 10f;
        [SerializeField, Header("Set the speed of coins in seconds to be displayed when pressing collect button")]
        float secondsBetweenResource = .05f;

        public GameObject coinSprite;
        public GameObject followerSprite;
        public Transform coinUITransform;
        public Transform followerUITransform;

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
            rewardText.text = rewardMessage.CurrencyReward.ToString();
            followersRewardText.text = rewardMessage.FollowersReward.ToString();
        }

        // void ShowReward(ShowRewardMessage rewardMessage) {
        //     GetComponent<Canvas>().enabled = true;
        //     currencyReward = rewardMessage.CurrencyReward;
        //     followersReward = rewardMessage.FollowersReward;
        //     //TODO: Currency and Followers!
        //     rewardText.text = rewardMessage.CurrencyReward.ToString();
        //     followersRewardText.text = rewardMessage.FollowersReward.ToString();
        // }

        public void CollectReward() {
            EventBroker.Instance().SendMessage(new EventUpdateCoins(currencyReward));
            EventBroker.Instance().SendMessage(new UpdateFollowersMessage(followersReward));
            EventBroker.Instance().SendMessage(new RemoveAllClothes());
            mainMenuButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
            StartCoroutine(SpawnCoins(currencyReward / animatedResourceDivider));
            StartCoroutine(SpawnFollowers(currencyReward / animatedResourceDivider));
        }

        IEnumerator SpawnCoins(float quantity) {
            for (var i = 0; i < quantity; i++) {
                var go = Instantiate(coinSprite, coinUITransform);
                go.transform.SetParent(coinUITransform);
                yield return new WaitForSeconds(secondsBetweenResource);
            }
        }

        IEnumerator SpawnFollowers(float quantity) {
            for (var i = 0; i < quantity; i++) {
                var go = Instantiate(followerSprite, followerUITransform);
                go.transform.SetParent(coinUITransform);
                yield return new WaitForSeconds(secondsBetweenResource);
            }
        }
    }
}