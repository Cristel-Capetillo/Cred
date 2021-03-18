using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorAndClothingType : IMissionRequirement{
        public MatchColorAndClothingType(ColorData colorData, ClothingType clothingType){
            ColorData = colorData;
            ClothingType = clothingType;
        }
        public ColorData ColorData { get; private set; }
        public ClothingType ClothingType{ get; private set; }
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) && wearable.ClothingType == ClothingType;
        }
    }
}