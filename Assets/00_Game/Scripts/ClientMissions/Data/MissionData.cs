using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClientMissions.Requirements;


namespace ClientMissions.Data {
    public class MissionData {
        public MissionDifficulty Difficulty{ get; }
        public ReadOnlyCollection<IMissionRequirement> Requirements{ get; }
        public StylePointValues StylePointValues{ get; }
        public ClientData ClientData{ get; }
        public SavableDialogData SavableDialogData{ get; }
        public SavableMissionData SavableMissionData{ get; }
        public MissionData(MissionDifficulty difficulty, List<IMissionRequirement> requirements, StylePointValues stylePointValues, ClientData clientData, SavableDialogData savableDialogData, SavableMissionData savableMissionData) {
            Difficulty = difficulty;
            Requirements = requirements.AsReadOnly();
            StylePointValues = stylePointValues;
            ClientData = clientData;
            SavableDialogData = savableDialogData;
            SavableMissionData = savableMissionData;
        }
    }
}
