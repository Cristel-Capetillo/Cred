using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using Clothing;

namespace Club.MissionRequirments{
    public class MatchColor: IMissionRequirement{
        public readonly ColorData ColorData;
        public MatchColor(ColorData colorData) {
            ColorData = colorData;
        }

        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData);
        }
        public override string ToString(){
            return $"Any {ColorData.name.ToLower()} wearable.";
        }
    }
}