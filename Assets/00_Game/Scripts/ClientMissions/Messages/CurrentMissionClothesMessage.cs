using System.Collections.Generic;
using Clothing;

namespace ClientMissions.Messages{
    public class CurrentMissionClothesMessage{
        public readonly Dictionary<ClothingType, CombinedWearables> CurrentWearables;
        public CurrentMissionClothesMessage(Dictionary<ClothingType, CombinedWearables> currentWearables){
            CurrentWearables = currentWearables;
        }
    }
}