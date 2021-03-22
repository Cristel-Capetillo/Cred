using System.Collections.Generic;
using ClientMissions.MissionRequirements;

namespace ClientMissions.Data {
    public class MissionData {
        public MissionDifficulty Difficulty{ get; }
        public List<IMissionRequirement> Requirements{ get; }
        public StylePointValues StylePointValues{ get; }
        public ClientTestData ClientTestData{ get; }
        public SavableDialogData SavableDialogData{ get; }
        public SavableMissionData SavableMissionData{ get; }
        public MissionData(MissionDifficulty difficulty, List<IMissionRequirement> requirements, StylePointValues stylePointValues, ClientTestData clientTestData, SavableDialogData savableDialogData, SavableMissionData savableMissionData) {
            Difficulty = difficulty;
            Requirements = requirements;
            StylePointValues = stylePointValues;
            ClientTestData = clientTestData;
            SavableDialogData = savableDialogData;
            SavableMissionData = savableMissionData;
        }
    }
}
