using System.Collections.Generic;
using Clothing;

namespace MysteryBox {
    public class EventMysteryBoxOpened {

        CombinedWearables reward;
        public EventMysteryBoxOpened(CombinedWearables reward) {
            
            this.reward = reward;
            
            UnityEngine.Analytics.Analytics.CustomEvent(
                "MysteryBoxOpened",
                new Dictionary<string, object> {
                    {"Mystery Box Reward", reward.ToString()}
                });
        }
    }
}