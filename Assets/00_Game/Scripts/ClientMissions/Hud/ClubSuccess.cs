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
        [SerializeField] Text followersRewardText;
        [SerializeField] Button collectButton;
        [SerializeField] Button mainMenuButton;

        int currencyReward;
        int followersReward;
        
        [SerializeField] [Tooltip("Set the amount of coins to be displayed when pressing collect button")]
        float animatedCoinsDivider = 10f;
        public float secondsBetweenCoins = .05f;
        public GameObject coinItem;
        public Transform coinFeatures;

      
        void Start() {
            //EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(ShowReward);
        }

        void OnDestroy() {
            //EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(ShowReward);
        }
        
        public void ShowReward(int currencyReward, int followersMessage) {
            GetComponent<Canvas>().enabled = true;
            this.currencyReward = currencyReward;
            followersReward = followersMessage;
            //TODO: Currency and Followers!
            rewardText.text = currencyReward.ToString();
            followersRewardText.text = followersMessage.ToString();
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
    }
}
