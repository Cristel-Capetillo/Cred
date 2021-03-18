using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorClothingTypeAndRarity : IMissionRequirement{
        public MatchColorClothingTypeAndRarity(ColorData colorData, ClothingType clothingType, Rarity rarity){
            ColorData = colorData;
            ClothingType = clothingType;
            Rarity = rarity;
        }

        public ColorData ColorData{ get; private set; }
        public ClothingType ClothingType{ get; private set; }
        public Rarity Rarity{ get; private set; }

        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) &&
                   wearable.ClothingType == ClothingType && wearable.Rarity == Rarity;
        }
    }
}