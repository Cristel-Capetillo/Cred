using System.Collections.Generic;
using ClientMissions.MissionRequirements;

namespace ClientMissions.Data {
    public class MissionData {
        public MissionDifficulty Difficulty{ get; private set; }
        public List<IMissionRequirement> Requirements{ get; private set; }
        public StylePointValues StylePointValues{ get; private set;}

        public MissionData(MissionDifficulty difficulty, List<IMissionRequirement> requirements, StylePointValues stylePointValues) {
            Difficulty = difficulty;
            Requirements = requirements;
            StylePointValues = stylePointValues;
        }
    }
}