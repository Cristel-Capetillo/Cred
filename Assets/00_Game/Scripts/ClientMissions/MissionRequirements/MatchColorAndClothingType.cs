using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorAndClothingType : IMissionRequirement{
        public readonly ColorData ColorData;
        public readonly ClothingType ClothingType;
        public MatchColorAndClothingType(ColorData colorData, ClothingType clothingType){
            ColorData = colorData;
            ClothingType = clothingType;
        }

        
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) && wearable.ClothingType == ClothingType;
        }

        public override string ToString(){
            return $"{ColorData.name} {ClothingType.SingularName.ToLower()}.";
        }
    }
}