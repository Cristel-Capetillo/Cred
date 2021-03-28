using System.Linq;
using ClientMissions.Data;
using Clothing;

namespace ClientMissions.Requirements{
    public class MatchColor: IMissionRequirement{
        public readonly ColorData ColorData;
        public MatchColor(ColorData colorData) {
            ColorData = colorData;
        }

        public bool PassedRequirement(CombinedWearables combinedWearables){
            return combinedWearables.wearable.Any(wearable => wearable.colorData == ColorData);
        }
        public override string ToString(){
            return $"Any {ColorData.name.ToLower()} clothing.";
        }
    }
}