using System.Collections.Generic;
using Clothing;
using Currency.Coins;
using UnityEngine;
using Utilities;

namespace HUD.Donate {
    public class DonationAlternatives : MonoBehaviour{
        DonationHandler donationHandler;
        CombinedWearables combinedWearables;
        int[] donatedCoins = { -1000, -2000, -3000 };
        
        
        public void DonateAlternative1() {
            donationHandler.DonateMeBaby(combinedWearables, 1);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[0]));
        }

        public void DonateAlternative2() {
            donationHandler.DonateMeBaby(combinedWearables, 2);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[1]));
        }

        public void DonateAlternative3() {
            donationHandler.DonateMeBaby(combinedWearables, 3);
            EventBroker.Instance().SendMessage(new EventUpdateCoins(donatedCoins[2]));
        }
    }
}