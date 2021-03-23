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

        //TODO it's wrong! Should probably be CombinedWearbles + check for all parts?
        public bool PassedRequirement(Wearable wearable){
            return wearable.colorData == ColorData &&
                   wearable.BodyPart == BodyPart;// && wearable.Rarity == Rarity;
        }
        public override string ToString(){
            return $"{ColorData.name} {Rarity.name.ToLower()} {BodyPart.SingularName.ToLower()}.";
        }
    }
}