using System.Linq;
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

        //TODO it's wrong! Should probably be CombinedWearbles + check for all parts?
        public bool PassedRequirement(CombinedWearables combinedWearables){
            return combinedWearables.rarity == Rarity && combinedWearables.clothingType == ClothingType && 
                   combinedWearables.wearable.Any(wearable => wearable.colorData == ColorData);
        }
        public override string ToString(){
            return $"{ColorData.name} {Rarity.name.ToLower()} {ClothingType.SingularName.ToLower()}.";
        }
    }
}