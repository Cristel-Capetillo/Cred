using Ads;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    public class DoubleRewardsButton : MonoBehaviour {
        Button button;

        void Start() {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => FindObjectOfType<AdsManager>().ShowRewardedAd(true));
        }
    }
}