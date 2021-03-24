using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable]
    public class SavableDialogData{
        [SerializeField] int clubIndex;
        [SerializeField] int dialogIndex;

        public SavableDialogData(int clubIndex, int dialogIndex){
            this.clubIndex = clubIndex;
            this.dialogIndex = dialogIndex;
        }

        public int ClubIndex => clubIndex;

        public int DialogIndex => dialogIndex;
    }

    [Serializable]
    public class SavableRequirementData{
        [SerializeField] int requirementValue;
        [SerializeField] List<int> requirementsDataIndex;

        public SavableRequirementData(int requirementValue, List<int> requirementsDataIndex){
            this.requirementValue = requirementValue;
            this.requirementsDataIndex = requirementsDataIndex;
        }

        public int RequirementValue => requirementValue;
        public List<int> RequirementsDataIndex => requirementsDataIndex;
    }
}