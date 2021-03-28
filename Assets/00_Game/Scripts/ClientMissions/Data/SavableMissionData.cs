using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClientMissions.Data{
    [Serializable]
    public class SavableMissionData{
        [SerializeField] int missionDifficultyIndex;
        [SerializeField] int missionClientIndex;
        [SerializeField] SavableDialogData savableDialogData;
        [SerializeField] List<SavableRequirementData> savableRequirementData;
        [SerializeField] long unixUtcTimeStamp;
        
        public SavableMissionData(int missionDifficultyIndex, int missionClientIndex,
            SavableDialogData savableDialogData, List<SavableRequirementData> savableRequirementData, long unixUtcTimeStamp){
            this.missionDifficultyIndex = missionDifficultyIndex; 
            this.missionClientIndex = missionClientIndex;
            this.savableDialogData = savableDialogData;//TODO: Split this up more?
            this.savableRequirementData = savableRequirementData;//TODO: Split this up more?
            this.unixUtcTimeStamp = unixUtcTimeStamp;
        }
        public int MissionDifficultyIndex => missionDifficultyIndex;
        public int MissionClientIndex => missionClientIndex;
        public SavableDialogData SavableDialogData => savableDialogData;
        public ReadOnlyCollection<SavableRequirementData> SavableRequirementData => savableRequirementData.AsReadOnly();

        public long UnixUtcTimeStamp => unixUtcTimeStamp;
    }
}