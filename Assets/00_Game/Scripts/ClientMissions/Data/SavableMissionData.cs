using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable]
    public class SavableMissionData{
        [SerializeField] int missionDifficultyIndex;
        [SerializeField] int missionClientIndex;
        [SerializeField] SavableDialogData savableDialogData;
        [SerializeField] List<SavableRequirementData> savableRequirementData;
        
        public SavableMissionData(int missionDifficultyIndex, int missionClientIndex,
            SavableDialogData savableDialogData, List<SavableRequirementData> savableRequirementData){
            this.missionDifficultyIndex = missionDifficultyIndex; 
            this.missionClientIndex = missionClientIndex;
            this.savableDialogData = savableDialogData;//TODO: Split this up more?
            this.savableRequirementData = savableRequirementData;//TODO: Split this up more?
        }
        public int MissionDifficultyIndex => missionDifficultyIndex;
        public int MissionClientIndex => missionClientIndex;
        public SavableDialogData SavableDialogData => savableDialogData;
        public List<SavableRequirementData> SavableRequirementData => savableRequirementData;
    }
}