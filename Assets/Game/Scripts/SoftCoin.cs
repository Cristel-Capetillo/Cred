using UnityEngine;
using UnityEngine.UI;

namespace Cred.Scripts
{
    public class SoftCoin : MonoBehaviour {
        public float coins = 0;
        public Text coinText;

        void Start() {
            coinText.text = coins.ToString();
        }

        public void getCoins(float value) {
            coins += value;
            coinText.text = coins.ToString();
        }

        public void loseCoins(float value) {
            coins -= value;
            coinText.text = coins.ToString();
        }
    }
}
