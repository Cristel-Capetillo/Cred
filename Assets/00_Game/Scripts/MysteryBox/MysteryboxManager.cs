using Currency.Coins;
using UnityEngine;
using Utilities;

namespace MysteryBox {
    public class MysteryboxManager : MonoBehaviour {
        public int coolDown; // Supposed to be 2 hours 
        [SerializeField] int cost;


        void Start() {
            EventBroker.Instance().SubscribeMessage<EventMysteryboxBought>(CanAffordMysterbox);
        }

        void CanAffordMysterbox(EventMysteryboxBought eventMysterboxBought) {
            if (FindObjectOfType<Coin>().Coins >= cost && coolDown <= 0) {
                FindObjectOfType<Coin>().Coins -= cost;
                Debug.Log("You could afford it");
            }
            else {
                Debug.Log("You could not afford it!");
            }
        }

        public void BuyMysteryBox() {
            EventBroker.Instance().SendMessage(new EventMysteryboxBought(cost));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventMysteryboxBought>(CanAffordMysterbox);
        }
    }
}
