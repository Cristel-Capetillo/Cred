using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Club {
    public class MissionGenerator : MonoBehaviour {
        //TODO: New user = pick tutorial mission...
        [SerializeField] MissionGeneratorData generatorData;
        [SerializeField] int testFollowers = 0;
        int currentCycleIndex;
        readonly Dictionary<int, List<int>> missionCycleCombinedList = new Dictionary<int, List<int>>();
        
        void Start(){
            AddMissionCyclesToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle, generatorData.MediumModeMissionCycle, generatorData.HardModeMissionCycle});
        }
        public MissionData CreateMissionData() {
            var missionDifficulty = PickDifficultyMission();
            var missionRequirements = CreateRandomRequirements(missionDifficulty.NumberOfRequirements);
            CycleIndex();//TODO: Save/load currentCycleIndex from firebase!
            foreach (var requirement in missionRequirements){
                Debug.Log(requirement.GetType());
            }
            return new MissionData(missionDifficulty,missionRequirements,null);
        }

        
        
        List<IMissionRequirement> CreateRandomRequirements(int requirementAmount){
            var missionRequirements = new List<IMissionRequirement>();
            //TODO: Fix this logic:
            var requirementAmountLeft = requirementAmount;
            while (requirementAmountLeft >= 1){
                var requirementValue = Random.Range(1, requirementAmountLeft);
                
                switch (requirementValue){
                    case 1:
                        missionRequirements.Add(new MatchColor(generatorData.Colors[Random.Range(0,generatorData.Colors.Count)]));
                        break;
                    case 2:
                        missionRequirements.Add(new MatchColorAndClothingType(
                            generatorData.Colors[Random.Range(0, generatorData.Colors.Count)],
                            generatorData.ClothingTypes[Random.Range(0, generatorData.ClothingTypes.Count)]));
                        break;
                    case 3:
                        missionRequirements.Add(new MatchColorClothingTypeAndRarity(generatorData.Colors[Random.Range(0, generatorData.Colors.Count)],
                            generatorData.ClothingTypes[Random.Range(0, generatorData.ClothingTypes.Count)],
                            generatorData.Rarities[Random.Range(1,generatorData.Rarities.Count)]));
                        break;
                }
                requirementAmountLeft -= requirementValue;
            }
            return missionRequirements;
        }
        MissionDifficulty PickDifficultyMission(){
            return generatorData.MissionDifficulties[missionCycleCombinedList[GetDifficultyKey()][currentCycleIndex]];
        }
        void AddMissionCyclesToDictionary(IReadOnlyList<List<int>> difficultyMissionLists){
            for (var i = 0; i < difficultyMissionLists.Count; i++){
                missionCycleCombinedList.Add(i,difficultyMissionLists[i]);
            }
        }
        void CycleIndex(){
            if (currentCycleIndex < 9)
                currentCycleIndex++;
            else{
                currentCycleIndex = 0;
            }
        }
        int GetDifficultyKey(){
            if (testFollowers <= generatorData.EasyModeEndValue)
                return 0;
            return testFollowers >= generatorData.HardModeStartValue ? 2 : 1;
        }
    }
}