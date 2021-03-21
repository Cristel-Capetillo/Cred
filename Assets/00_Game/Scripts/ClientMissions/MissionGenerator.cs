using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.Helpers;
using UnityEngine;

namespace ClientMissions {
    public class MissionGenerator{
        readonly Dictionary<int, List<int>> missionCycles;
        MissionGeneratorData generatorData;
        IPlayer playerData;
        int missionCycleCount;

        public int MissionCycleCount => missionCycleCount;

        public MissionGenerator(MissionGeneratorData generatorData, IPlayer player){
            this.generatorData = generatorData;
             missionCycles = Helper.CombineListsToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle, 
                 generatorData.MediumModeMissionCycle, generatorData.HardModeMissionCycle});
             missionCycleCount = Helper.GetLowestNumberFromThreeNumbers(generatorData.EasyModeMissionCycle.Count,
                 generatorData.MediumModeMissionCycle.Count, generatorData.HardModeMissionCycle.Count);
             playerData = player;
        }
        public SavableMissionData GenerateSavableMissionData(){
            var missionDifficultyIndex = GetDifficultyIndex(playerData.MissionIndex);
            var missionRequirements = generatorData.MissionDifficulties[missionDifficultyIndex].NumberOfRequirements;
            var savableRequirementData = GenerateNewRequirements(missionRequirements);
            var missionClient = generatorData.ClientData[playerData.ClientIndex];
            var clientIndex = playerData.ClientIndex;
            CycleIndexes();
            return new SavableMissionData(missionDifficultyIndex, clientIndex, 
                new SavableDialogData(Random.Range(0,missionClient.StartDialog.Count), Random.Range(0,missionClient.MissionInfoDialog.Count)), savableRequirementData);
        }
        int GetDifficultyIndex(int difficultyIndex){
            return missionCycles[GetDifficultyCycleKey()][difficultyIndex];
        }
        int GetDifficultyCycleKey(){
            if (playerData.Followers <= generatorData.EasyModeEndValue)
                return 0;
            return playerData.Followers >= generatorData.HardModeStartValue ? 2 : 1;
        }
        void CycleIndexes(){
            playerData.ClientIndex = Helper.CycleListIndex(playerData.ClientIndex, generatorData.ClientData.Count);
            playerData.MissionIndex = Helper.CycleListIndex(playerData.MissionIndex, missionCycleCount);
        }
        List<SavableRequirementData> GenerateNewRequirements(int requirementAmount){
            var savableMissionRequirements = new List<SavableRequirementData>();
            var requirementAmountLeft = requirementAmount;
            var colorDataList = Helper.CreateListOfIndexes(generatorData.Colors.Count);
            var clothingTypeDataList = Helper.CreateListOfIndexes(generatorData.ClothingTypes.Count);
            while (requirementAmountLeft >= 1){
                var requirementValue = Helper.NumberGenerator(Mathf.Min(3,requirementAmountLeft));
                var colorDataListIndex = GetRandomNonRepeatingIndexFromList(colorDataList);
                var clothingTypeIndex = GetRandomNonRepeatingIndexFromList(clothingTypeDataList);

                switch (requirementValue){
                    case 1:
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue,
                            new List<int>{colorDataListIndex}));
                        break;
                    case 2:
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue,
                            new List<int>{colorDataListIndex, clothingTypeIndex}));
                        break;
                    case 3:{
                        var rarityDataIndex = Random.Range(1, generatorData.Rarities.Count);
                        savableMissionRequirements.Add(new SavableRequirementData(requirementValue,
                            new List<int>{colorDataListIndex, clothingTypeIndex, rarityDataIndex}));
                        break;
                    }
                }

                requirementAmountLeft -= requirementValue;
            }
            return savableMissionRequirements;
        }
        static int GetRandomNonRepeatingIndexFromList(List<int> listOfIndexes){
            var randomListIndex = Random.Range(0, listOfIndexes.Count);
            var index = listOfIndexes[randomListIndex];
            listOfIndexes.RemoveAt(randomListIndex);
            return index;
        }
    }
}