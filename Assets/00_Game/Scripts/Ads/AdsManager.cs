using EventBrokerFolder;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Cred._00_Game.Scripts.Ads {
    public class AdsManager : MonoBehaviour, IUnityAdsListener {
        void Start() {
            Advertisement.AddListener(this);
            Advertisement.Initialize("4044681", true);
        }

        public void ShowAd(string adID) {
            Advertisement.Show(adID);
        }

        public void OnUnityAdsReady(string placementId) {
            // Make button show that ad is ready?
            Debug.Log("Ad is ready");
        }

        public void OnUnityAdsDidError(string message) {
            Debug.Log("Ad got an error");
        }

        public void OnUnityAdsDidStart(string placementId) {
            // Stuff to do when ad starts
            Debug.Log("Ad started");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            if (showResult == ShowResult.Finished) {
                Debug.Log("Successfully watched an ad");
                // EventBroker.Instance().SendMessage(new EventIncreaseCoins(_coinsReward));
            }
            else if (showResult == ShowResult.Failed) {
                Debug.Log("Error while trying to watch ad");
            }
            else if (showResult == ShowResult.Skipped){
                Debug.Log("Ha ha u tried to skip the ad I see.. no reward for u p*ssy");
            }
        }
    }
}
