using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorClothingTypeAndRarity : IMissionRequirement{
        public MatchColorClothingTypeAndRarity(ColorData colorData, BodyPart bodyPart, Rarity rarity){
            ColorData = colorData;
            BodyPart = bodyPart;
            Rarity = rarity;
        }

        public ColorData ColorData{ get; private set; }
        public BodyPart BodyPart{ get; private set; }
        public Rarity Rarity{ get; private set; }

        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) &&
                   wearable.BodyPart == BodyPart && wearable.Rarity == Rarity;
        }
        public override string ToString(){
            return $"{ColorData.name} {Rarity.name.ToLower()} {BodyPart.SingularName.ToLower()}.";
        }
    }
}