using System.Linq;
using ClientMissions.Data;
using Clothing;

namespace ClientMissions.Requirements{
    public class MatchColorClothingTypeAndRarity : IMissionRequirement{
        public MatchColorClothingTypeAndRarity(ColorData colorData, ClothingType clothingType, Rarity rarity){
            ColorData = colorData;
            ClothingType = clothingType;
            Rarity = rarity;
        }

        public ColorData ColorData{ get;}
        public ClothingType ClothingType{ get;}
        public Rarity Rarity{ get;}
        
        public bool PassedRequirement(CombinedWearables combinedWearables){
            return combinedWearables.rarity == Rarity && combinedWearables.clothingType == ClothingType && 
                   combinedWearables.wearable.Any(wearable => wearable.colorData == ColorData);
        }
 
        public override string ToString(){
            return $"{ColorData.name} {Rarity.name.ToLower()} {ClothingType.SingularName.ToLower()}.";
        }
    }
}