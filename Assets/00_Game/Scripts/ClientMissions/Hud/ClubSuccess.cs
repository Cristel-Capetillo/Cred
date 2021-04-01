using ClientMissions.Messages;
using Clothing.DressUp;
using Currency.Coins;
using UnityEngine; 
using UnityEngine.UI;
using Utilities;
using System.Collections;

namespace ClientMissions.Hud {
    public class ClubSuccess : MonoBehaviour {
        [SerializeField] Text rewardText;
        [SerializeField] Text coinUIText;
        [SerializeField] Text followersRewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        Coin coin;
        int currencyReward;
        int followersReward;
        
        [SerializeField] [Tooltip("Set the amount of coins to be displayed when pressing collect button")]
        float animatedCoinsDivider = 10f;
        public float secondsBetweenCoins = .05f;
        public float secondsBetweenCoinUIUpdate = .001f;
        public GameObject coinItem;
        public Transform coinFeatures;

      
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
        public void CollectReward() {
            coin = FindObjectOfType<Coin>();
            EventBroker.Instance().SendMessage(new EventUpdateCoins(currencyReward));
            StartCoroutine(UpdateCoinAmount(currencyReward));
            EventBroker.Instance().SendMessage(new UpdateFollowersMessage(followersReward));
            EventBroker.Instance().SendMessage(new RemoveAllClothes());
            mainMenuButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
            StartCoroutine(SpawnCoins(currencyReward/animatedCoinsDivider));
        }
        

        IEnumerator SpawnCoins(float quantity)
        {
            for(int i = 0; i < quantity; i++)
            {
                GameObject go = Instantiate(coinItem, coinFeatures);
                go.transform.SetParent(coinFeatures);
                yield return new WaitForSeconds(secondsBetweenCoins);
            }
        }

        IEnumerator UpdateCoinAmount(int quantity)
        {
            int currentCoinAmount = coin.Coins - quantity;

            while (currentCoinAmount < coin.Coins)
            {
                currentCoinAmount++;
                coinUIText.text = currentCoinAmount.ToString();

                yield return new WaitForSeconds(secondsBetweenCoinUIUpdate);
            }
        }
    }
}
