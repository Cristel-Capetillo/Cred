using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using Clothing;

namespace Club.MissionRequirments{
    public class MatchColor: IMissionRequirement{
        public MatchColor(ColorData colorData) {
            ColorData = colorData;
        }
        public ColorData ColorData { get; private set; }
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData);
        }
    }
}