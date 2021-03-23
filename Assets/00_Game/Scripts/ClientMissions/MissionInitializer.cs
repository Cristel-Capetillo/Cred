using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.MissionRequirements;
using Club.MissionRequirments;
using UnityEngine;

namespace ClientMissions{
    public class MissionInitializer : MonoBehaviour
    {
        [SerializeField] MissionGeneratorData generatorData;//TODO: Get this from addressable for possible remote balancing?
        [SerializeField] LocalPlayerTestData localPlayerTestData;
        public MissionGenerator CreateMissionGenerator(){
            return new MissionGenerator(generatorData, localPlayerTestData);
        }

        public IMissionHolder GetMissionHolder(){
            return localPlayerTestData;
        }
        public MissionData GetSavedMission(SavableMissionData savableMissionData){
            var missionDifficulty = generatorData.MissionDifficulties[savableMissionData.MissionDifficultyIndex];
            var missionClient = generatorData.ClientData[savableMissionData.MissionClientIndex];
            
            return new MissionData(missionDifficulty,LoadRequirements(savableMissionData.SavableRequirementData), 
                new StylePointValues(missionDifficulty.MinimumStylePoints, missionDifficulty.MaximumStylePoints), 
                missionClient,savableMissionData.SavableDialogData, savableMissionData);
        }
        List<IMissionRequirement> LoadRequirements(IEnumerable<SavableRequirementData> savableRequirementData){
            var missionRequirements = new List<IMissionRequirement>();
            foreach (var requirementData in savableRequirementData){
                switch (requirementData.RequirementValue){
                    case 1:
                       missionRequirements.Add(new MatchColor(generatorData.Colors[requirementData.RequirementsDataIndex[0]])); 
                       //MatchColor.FromSavable(requirementData)
                        break;
                    case 2:
                        missionRequirements.Add(new MatchColorAndClothingType(generatorData.Colors[requirementData.RequirementsDataIndex[0]],
                            generatorData.ClothingTypes[requirementData.RequirementsDataIndex[1]]));
                        break;
                    case 3:
                        missionRequirements.Add(new MatchColorClothingTypeAndRarity(
                            generatorData.Colors[requirementData.RequirementsDataIndex[0]],
                            generatorData.ClothingTypes[requirementData.RequirementsDataIndex[1]], generatorData.Rarities[requirementData.RequirementsDataIndex[2]]));
                        break;
                }
            }
            return missionRequirements;
        }
    }
}
