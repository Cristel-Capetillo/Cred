using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.Helpers;
using ClientMissions.MissionRequirements;
using Club.MissionRequirments;
using UnityEngine;

namespace ClientMissions {
    public class MissionGenerator : MonoBehaviour {
        //TODO: New user = pick tutorial mission...
        [SerializeField] MissionGeneratorData generatorData;
        [SerializeField] int testFollowers = 0;
        [SerializeField] int maxFollowers = 1000;
        readonly Dictionary<int, List<int>> missionCycleCombinedList = new Dictionary<int, List<int>>();
        int currentMissionDifficultyIndex;
        int missionCycleCount;
        int currentClientIndex;

        void Start(){
            AddMissionCyclesToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle, 
                generatorData.MediumModeMissionCycle, generatorData.HardModeMissionCycle});
            missionCycleCount = Mathf.Min(Mathf.Min(generatorData.EasyModeMissionCycle.Count,
                generatorData.MediumModeMissionCycle.Count), generatorData.HardModeMissionCycle.Count);
        }
        
        public SavableMissionData GenerateMissionData() {
            var savableRequirementData = GenerateNewRequirements(PickDifficultyMission(currentMissionDifficultyIndex).NumberOfRequirements);
            var missionClient = generatorData.ClientData[currentClientIndex];
            currentClientIndex = SemiRandom.CycleListIndex(currentClientIndex, generatorData.ClientData.Count);
            currentMissionDifficultyIndex = SemiRandom.CycleListIndex(currentMissionDifficultyIndex, missionCycleCount);
            return new SavableMissionData(currentMissionDifficultyIndex, currentClientIndex, 
                new SavableDialogData(SemiRandom.RandomIndex(missionClient.StartDialog.Count), SemiRandom.RandomIndex(missionClient.MissionInfoDialog.Count)), savableRequirementData);
        }
        public MissionData GenerateMission(SavableMissionData savableMissionData){
            var missionDifficulty = generatorData.MissionDifficulties[savableMissionData.MissionDifficultyIndex];
            var missionClient = generatorData.ClientData[savableMissionData.MissionClientIndex];
            return new MissionData(missionDifficulty,LoadRequirements(savableMissionData.SavableRequirementData), 
                new StylePointValues(missionDifficulty.MinimumStylePoints, missionDifficulty.MaximumStylePoints), 
                missionClient,savableMissionData.SavableDialogData);
        }

        StylePointValues CreateStylePointValues(MissionDifficulty missionDifficulty){
            var (min, max) = AdjustStylePoint(missionDifficulty.MinimumStylePoints,
                missionDifficulty.MaximumStylePoints);
            return new StylePointValues(min,max);
        }
        (int, int) AdjustStylePoint(int minValue, int maxValue){
            const int maxPossibleStylePoints = 50;
            var t = Mathf.InverseLerp(0, maxFollowers, testFollowers);
            minValue = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, t));
            maxValue = Mathf.RoundToInt(Mathf.Lerp(maxValue, maxPossibleStylePoints,t)); 
            return (minValue, maxValue);
        }

        List<IMissionRequirement> LoadRequirements(List<SavableRequirementData> savableRequirementData){
            var missionRequirements = new List<IMissionRequirement>();
            foreach (var requirementData in savableRequirementData){
                switch (requirementData.RequirementValue){
                case 1:
                    missionRequirements.Add(new MatchColor(generatorData.Colors[requirementData.RequirementsDataIndex[0]]));
                    break;
                case 2:
                    missionRequirements.Add(new MatchColorAndClothingType(generatorData.Colors[requirementData.RequirementsDataIndex[0]],
                        generatorData.ClothingTypes[requirementData.RequirementsDataIndex[1]]));
                    break;
                case 3:
                    missionRequirements.Add(new MatchColorClothingTypeAndRarity(
                        generatorData.Colors[requirementData.RequirementsDataIndex[0]],
                        generatorData.ClothingTypes[requirementData.RequirementsDataIndex[1]], generatorData.Rarities[requirementData.RequirementsDataIndex[3]]));
                    break;
                }
            }
            return missionRequirements;
        }
        List<SavableRequirementData> GenerateNewRequirements(int requirementAmount){
            var savableMissionRequirements = new List<SavableRequirementData>();
            var requirementAmountLeft = requirementAmount;
            var colorDataList = new List<ColorData>();
            colorDataList.AddRange(generatorData.Colors);
            while (requirementAmountLeft >= 1){
                var requirementValue = SemiRandom.NumberGenerator(requirementAmountLeft);
                int colorDataIndex;
                int clothingTypeIndex;
                switch (requirementValue){
                    case 1:
                        colorDataIndex = SemiRandom.AddColorVariation(colorDataList);
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue, new List<int>(){colorDataIndex}));
                        break;
                    case 2:
                        clothingTypeIndex = SemiRandom.RandomIndex(generatorData.ClothingTypes.Count);
                        colorDataIndex = SemiRandom.AddColorVariation(colorDataList);
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue,new List<int>{colorDataIndex,clothingTypeIndex}));
                        
                        break;
                    case 3:
                        colorDataIndex = SemiRandom.RandomIndex(generatorData.ClothingTypes.Count);
                        clothingTypeIndex = SemiRandom.RandomIndex(generatorData.ClothingTypes.Count);
                        var rarityDataIndex = SemiRandom.RandomIndex(generatorData.Rarities.Count);
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue,new List<int>{colorDataIndex, clothingTypeIndex, rarityDataIndex}));
                        break;
                }
                requirementAmountLeft -= requirementValue;
            }
            return savableMissionRequirements;
        }

        MissionDifficulty PickDifficultyMission(int difficultyIndex){
            return generatorData.MissionDifficulties[missionCycleCombinedList[GetDifficultyKey()][difficultyIndex]];
        }
        void AddMissionCyclesToDictionary(IReadOnlyList<List<int>> difficultyMissionLists){
            for (var i = 0; i < difficultyMissionLists.Count; i++){
                missionCycleCombinedList.Add(i,difficultyMissionLists[i]);
            }
        }
        int GetDifficultyKey(){
            if (testFollowers <= generatorData.EasyModeEndValue)
                return 0;
            return testFollowers >= generatorData.HardModeStartValue ? 2 : 1;
        }
    }
}