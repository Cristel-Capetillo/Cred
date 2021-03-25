using ClientMissions.Data;
using Clothing;
using Club.MissionRequirments;

namespace ClientMissions.MissionRequirements{
    public struct RequirementFactory{
        public static IMissionRequirement FromSavable(int requirementValue,ColorData colorData, ClothingType clothingType, Rarity rarity){
            return requirementValue switch{
                1 => new MatchColor(colorData),
                2 => new MatchColorAndClothingType(colorData, clothingType),
                3 => new MatchColorClothingTypeAndRarity(colorData, clothingType, rarity),
                _ => null
            };
        }
    }
}