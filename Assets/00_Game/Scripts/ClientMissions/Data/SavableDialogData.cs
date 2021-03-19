using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data {
    public class SavableDialogData{
        public readonly int StartDialogIndex;
        public readonly int MissionInfoIndex;
        public SavableDialogData(int startDialogIndex, int missionInfoIndex){
            StartDialogIndex = startDialogIndex;
            MissionInfoIndex = missionInfoIndex;
        }
    }

    public class SavableRequirementData{
        public readonly int RequirementValue;
        public readonly List<int> RequirementsDataIndex;
        
        public SavableRequirementData(int requirementValue,List<int> requirementsDataIndexIndex){
            RequirementValue = requirementValue;
            RequirementsDataIndex = requirementsDataIndexIndex;
        }
    }
}