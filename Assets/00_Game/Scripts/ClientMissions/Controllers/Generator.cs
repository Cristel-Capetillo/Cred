using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientMissions.Data;
using ClientMissions.Helpers;
using UnityEngine;
using Utilities.Time;
using Random = UnityEngine.Random;

namespace ClientMissions.Controllers {
    public class Generator{
        readonly Dictionary<int, List<int>> missionCycles;
        MissionGeneratorData generatorData;
        IPlayer playerData;
        int missionCycleCount;
        TimeManager timeManager;

        public Generator(MissionGeneratorData generatorData, IPlayer player, TimeManager timeManager){
            this.generatorData = generatorData;
             missionCycles = CollectionsHelper.CombineListsToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle.ToList(), 
                 generatorData.MediumModeMissionCycle.ToList(), generatorData.HardModeMissionCycle.ToList()});
             missionCycleCount = CalculationsHelper.GetLowestNumberFromThreeNumbers(generatorData.EasyModeMissionCycle.Count,
                 generatorData.MediumModeMissionCycle.Count, generatorData.HardModeMissionCycle.Count);
             playerData = player;
             this.timeManager = timeManager;
        }
        public SavableMissionData GenerateSavableMissionData(){
            var missionDifficultyIndex = GetDifficultyIndex(playerData.MissionIndex);
            var missionRequirements = generatorData.MissionDifficulties[missionDifficultyIndex].NumberOfRequirements;
            var savableRequirementData = GenerateNewRequirements(missionRequirements);
            var missionClient = generatorData.ClientData[playerData.ClientIndex];
            var clientIndex = playerData.ClientIndex;
            CycleIndexes();
            var randomClub = Random.Range(0, missionClient.ClientDialogData.Count);
            var randomDialog = Random.Range(0, missionClient.ClientDialogData[randomClub].Dialog.Count);
            var dataTimeStart1 = timeManager.timeHandler.GetTime();
            var unixTime = TimeDateConverter.ToUnixTimestamp(dataTimeStart1);
            
            return new SavableMissionData(missionDifficultyIndex, clientIndex, 
                new SavableDialogData(randomClub, randomDialog), savableRequirementData, unixTime);
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
            playerData.ClientIndex = CollectionsHelper.CycleListIndex(playerData.ClientIndex, generatorData.ClientData.Count);
            playerData.MissionIndex = CollectionsHelper.CycleListIndex(playerData.MissionIndex, missionCycleCount);
        }
        List<SavableRequirementData> GenerateNewRequirements(int requirementAmount){
            var savableMissionRequirements = new List<SavableRequirementData>();
            var requirementAmountLeft = requirementAmount;
            var colorDataList = CollectionsHelper.CreateListOfIndexes(generatorData.Colors.Count);
            var clothingTypeDataList = CollectionsHelper.CreateListOfIndexes(generatorData.ClothingTypes.Count);
            while (requirementAmountLeft >= 1){
                var requirementValue = CalculationsHelper.NumberGenerator(Mathf.Min(3,requirementAmountLeft));
                var colorDataListIndex = CollectionsHelper.GetRandomNonRepeatingIndexFromList(colorDataList);
                var clothingTypeIndex = CollectionsHelper.GetRandomNonRepeatingIndexFromList(clothingTypeDataList);
                var rarityDataIndex = Random.Range(1, generatorData.Rarities.Count);
                savableMissionRequirements.Add(new SavableRequirementData(requirementValue, 
                    new List<int>{colorDataListIndex, clothingTypeIndex, rarityDataIndex}));
                requirementAmountLeft -= requirementValue;
            }
            return savableMissionRequirements;
        }
    }
}