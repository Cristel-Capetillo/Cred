using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.Requirements;
using UnityEngine;
using Utilities.Time;

namespace ClientMissions.Controllers{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] MissionGeneratorData generatorData;//TODO: Get this from addressable for possible remote balancing?
        [SerializeField] LocalPlayer localPlayer;
        public Generator CreateMissionGenerator(){
            return new Generator(generatorData, localPlayer, FindObjectOfType<TimeManager>());
        }
        public ISavedMission GetMissionHolder(){
            return localPlayer;
        }
        public MissionData GetSavedMission(SavableMissionData savableMissionData){
            var missionDifficulty = generatorData.MissionDifficulties[savableMissionData.MissionDifficultyIndex];
            var missionClient = generatorData.ClientData[savableMissionData.MissionClientIndex];
            
            return new MissionData(missionDifficulty,LoadRequirements(savableMissionData.SavableRequirementData), 
                new StylePointValues(missionDifficulty.MinimumStylePoints, missionDifficulty.MaximumStylePoints), 
                missionClient,savableMissionData.SavableDialogData, savableMissionData);
        }
        List<IMissionRequirement> LoadRequirements(IEnumerable<SavableRequirementData> savableRequirementData){
            return savableRequirementData.Select(requirementData => RequirementFactory.FromSavable(requirementData.RequirementValue, 
                generatorData.Colors[requirementData.RequirementsDataIndex[0]], generatorData.ClothingTypes[requirementData.RequirementsDataIndex[1]], 
                generatorData.Rarities[requirementData.RequirementsDataIndex[2]])).ToList();
        }
    }
}
