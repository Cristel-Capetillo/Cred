using EventBrokerFolder;
using UnityEngine;
using UnityEngine.UI;

namespace Cred.Scripts
{
    public class Coin : MonoBehaviour {
        public int _coin;
        public Text coinText;

        public int Coins {
            get {
                return _coin;
            }
            set {
                _coin = value;
                EventBroker.Instance().SendMessage(new EventCoinChanged(_coin));
            }
        }
    }

    public class EventCoinChanged {
        public readonly int Coins;

        public EventCoinChanged(int coins) {
            Coins = coins;
        }
    }

    public class UpdateCoinTextOnScreen : MonoBehaviour {
        Coin coinClass;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventCoinChanged>(UpdateCoinText);
        }

        void UpdateCoinText(EventCoinChanged eventCoinChanged) {
            coinClass.coinText.text = coinClass._coin.ToString();
        }
    }
}
