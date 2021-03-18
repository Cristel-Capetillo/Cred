using Currency.Coins;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD {
    public class UpdateCoinTextOnScreen : MonoBehaviour {
        Text coinText;

        void Awake() {
            coinText = GetComponent<Text>();
        }

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventUpdateUICoins>(UpdateCoinText);
        }

        void UpdateCoinText(EventUpdateUICoins eventUpdateUICoins) {
            coinText.text = eventUpdateUICoins.Coins.ToString();
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateUICoins>(UpdateCoinText);
        }
    }
}