using ClientMissions.Data;
using Clothing;

namespace ClientMissions.MissionRequirements{
    public class MatchColorAndClothingType : IMissionRequirement{
        public MatchColorAndClothingType(ColorData colorData, BodyPart bodyPart){
            ColorData = colorData;
            BodyPart = bodyPart;
        }
        public ColorData ColorData { get; private set; }
        public BodyPart BodyPart{ get; private set; }
        
        //TODO it's wrong! Should probably be CombinedWearbles + check for all parts?
        public bool PassedRequirement(Wearable wearable){
            return wearable.colorData == ColorData && wearable.BodyPart == BodyPart;
        }

        public override string ToString(){
            return $"{ColorData.name} {BodyPart.SingularName.ToLower()}.";
        }
    }
}