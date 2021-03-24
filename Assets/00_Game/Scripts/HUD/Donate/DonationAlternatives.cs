using Clothing;
using Currency.Coins;
using UnityEngine;
using Utilities;

namespace HUD.Donate {
    public class DonationAlternatives : MonoBehaviour{
        DonationHandler donationHandler;
        CombinedWearables combinedWearables;
        
        
        public void DonateAlternative1() {
            donationHandler.UpgradeMeBaby(combinedWearables, 1);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(-1000));
        }

        public void DonateAlternative2() {
            donationHandler.UpgradeMeBaby(combinedWearables, 2);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(-2000));
        }

        public void DonateAlternative3() {
            donationHandler.UpgradeMeBaby(combinedWearables, 3);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(-3000));
        }
    }
}