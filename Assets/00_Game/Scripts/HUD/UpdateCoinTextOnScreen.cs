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
            EventBroker.Instance().SubscribeMessage<EventCoinChanged>(UpdateCoinText);
        }

        void UpdateCoinText(EventCoinChanged eventCoinChanged) {
            coinText.text = eventCoinChanged.Coins.ToString();
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventCoinChanged>(UpdateCoinText);
        }
    }
}