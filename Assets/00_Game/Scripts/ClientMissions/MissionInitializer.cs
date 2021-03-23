using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using Club.MissionRequirments;
using UnityEngine;
using Utilities.Time;

namespace ClientMissions{
    public class MissionInitializer : MonoBehaviour
    {
        [SerializeField] MissionGeneratorData generatorData;//TODO: Get this from addressable for possible remote balancing?
        [SerializeField] LocalPlayerTest localPlayerTest;
        public MissionGenerator CreateMissionGenerator(){
            return new MissionGenerator(generatorData, localPlayerTest, FindObjectOfType<TimeManager>());
        }

        public IMissionHolder GetMissionHolder(){
            return localPlayerTest;
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
