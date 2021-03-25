using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorAndClothingType : IMissionRequirement{
        public MatchColorAndClothingType(ColorData colorData, ClothingType clothingType){
            ColorData = colorData;
            ClothingType = clothingType;
        }
        public ColorData ColorData{ get; }
        public ClothingType ClothingType{ get;}
        
        public bool PassedRequirement(CombinedWearables combinedWearables){
            return combinedWearables.clothingType == ClothingType && combinedWearables.wearable.Any(wearable => wearable.colorData == ColorData);
        }
        public override string ToString(){
            return $"{ColorData.name} {ClothingType.SingularName.ToLower()}.";
        }
    }
}