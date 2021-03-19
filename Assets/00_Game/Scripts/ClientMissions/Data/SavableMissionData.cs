using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable]
    public class SavableMissionData{
        public readonly int MissionDifficultyIndex;
        public readonly int MissionClientIndex;
        public readonly SavableDialogData SavableDialogData;
        public readonly List<SavableRequirementData> SavableRequirementData;

        public SavableMissionData(int missionDifficultyIndex, int missionClientIndex,
            SavableDialogData savableDialogData, List<SavableRequirementData> savableRequirementData){
            MissionDifficultyIndex = missionDifficultyIndex; 
            MissionClientIndex = missionClientIndex;
            SavableDialogData = savableDialogData;//TODO: Split this up more?
            SavableRequirementData = savableRequirementData;//TODO: Split this up more?
        }
    }
}