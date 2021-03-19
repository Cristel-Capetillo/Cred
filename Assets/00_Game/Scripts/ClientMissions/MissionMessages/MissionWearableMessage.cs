using System.Collections.Generic;
using Clothing;

namespace ClientMissions.MissionMessages{
    public class MissionWearableMessage{

        public List<Wearable> wearables;
        
        public MissionWearableMessage(List<Wearable> wearables){
            this.wearables = wearables;
        }
    }
}