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
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) && wearable.BodyPart == BodyPart;
        }

        public override string ToString(){
            return $"{ColorData.name} {BodyPart.SingularName.ToLower()}.";
        }
    }
}