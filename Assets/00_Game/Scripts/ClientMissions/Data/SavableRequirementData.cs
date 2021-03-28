using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable] public class SavableRequirementData{
        [SerializeField] int requirementValue;
        [SerializeField] List<int> requirementsDataIndex;

        public SavableRequirementData(int requirementValue, List<int> requirementsDataIndex){
            this.requirementValue = requirementValue;
            this.requirementsDataIndex = requirementsDataIndex;
        }

        public int RequirementValue => requirementValue;
        public ReadOnlyCollection<int> RequirementsDataIndex => requirementsDataIndex.AsReadOnly();
    }
}