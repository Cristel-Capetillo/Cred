using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorClothingTypeAndRarity : IMissionRequirement{
        public readonly ColorData ColorData;
        public readonly ClothingType ClothingType;
        public readonly Rarity Rarity;
        public MatchColorClothingTypeAndRarity(ColorData colorData, ClothingType clothingType, Rarity rarity){
            ColorData = colorData;
            ClothingType = clothingType;
            Rarity = rarity;
        }
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) &&
                   wearable.ClothingType == ClothingType && wearable.Rarity == Rarity;
        }
        public override string ToString(){
            return $"{ColorData.name} {Rarity.name.ToLower()} {ClothingType.SingularName.ToLower()}.";
        }
    }
}