using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable]
    public class SavableDialogData{
        [SerializeField] int startDialogIndex;
        [SerializeField] int missionInfoIndex;

        public SavableDialogData(int startDialogIndex, int missionInfoIndex){
            this.startDialogIndex = startDialogIndex;
            this.missionInfoIndex = missionInfoIndex;
        }

        public int StartDialogIndex => startDialogIndex;

        public int MissionInfoIndex => missionInfoIndex;
    }

    [Serializable]
    public class SavableRequirementData{
        [SerializeField] int requirementValue;
        [SerializeField] List<int> requirementsDataIndex;

        public SavableRequirementData(int requirementValue, List<int> requirementsDataIndexIndex){
            this.requirementValue = requirementValue;
            this.requirementsDataIndex = requirementsDataIndexIndex;
        }

        public int RequirementValue => requirementValue;
        public List<int> RequirementsDataIndex => requirementsDataIndex;
    }
}