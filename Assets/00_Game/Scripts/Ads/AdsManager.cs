using UnityEngine;
using UnityEngine.Advertisements;
using Utilities;

namespace Ads {
    public class AdsManager : MonoBehaviour, IUnityAdsListener {
        string gameId = "4044681";
        string adID = "Rewarded_Android";
        
        void Start() { 
            if (Application.platform == RuntimePlatform.Android)
                gameId = "4044681";
            else if (Application.platform == RuntimePlatform.IPhonePlayer) {
                gameId = "4044680";
                adID = "Rewarded_iOS"; 
            }

            // If we have multiple scenes we need to put this somewhere else than in start
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, true);
        }

        void OnApplicationPause(bool pauseStatus) {
            if (!pauseStatus) {
                //resync time
            }
        }

        public void ShowRewardedAd() {
            Advertisement.Show(adID);
        }
        
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            if (showResult == ShowResult.Finished) {
                EventBroker.Instance().SendMessage(new EventAdWatched());
            }
            else if (showResult == ShowResult.Failed) {
                Debug.Log("Error while trying to watch ad");
            }
            else if (showResult == ShowResult.Skipped){
                Debug.Log("Ha ha u tried to skip the ad I see.. no reward for u p*ssy");
            }
        }
        public void OnUnityAdsReady(string placementId) {
            // Make a button/text display that an ad is ready?
        }
        public void OnUnityAdsDidError(string message) {
            Debug.Log("Ad got an error");
        }
        public void OnUnityAdsDidStart(string placementId) {
            // Stuff to do when the ad starts
            Debug.Log("Ad started");
        }
    }
}
