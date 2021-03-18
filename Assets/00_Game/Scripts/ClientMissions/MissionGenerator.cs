using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Club {
    public class MissionGenerator : MonoBehaviour {
        //TODO: New user = pick tutorial mission...
        [SerializeField] MissionGeneratorData generatorData;
        [SerializeField] int testFollowers = 0;
        [SerializeField] int maxFollowers = 1000; 
        int currentCycleIndex;
        readonly Dictionary<int, List<int>> missionCycleCombinedList = new Dictionary<int, List<int>>();
        
        void Start(){
            AddMissionCyclesToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle, generatorData.MediumModeMissionCycle, generatorData.HardModeMissionCycle});
        }
        public MissionData CreateMissionData() {
            var missionDifficulty = PickDifficultyMission();
            CycleIndex();//TODO: Save/load currentCycleIndex from firebase!
            return new MissionData(missionDifficulty,CreateRandomRequirements(missionDifficulty.NumberOfRequirements),CreateStylePointValues(missionDifficulty));
        }

        StylePointValues CreateStylePointValues(MissionDifficulty missionDifficulty){
            var (min, max) = AdjustStylePoint(missionDifficulty.MinimumStylePoints,missionDifficulty.MaximumStylePoints);
            return new StylePointValues(min,max);
        }
        (int, int) AdjustStylePoint(int minValue, int maxValue){
            const int maxPossibleStylePoints = 50;
            var t = Mathf.InverseLerp(0, maxFollowers, testFollowers);
            minValue = Mathf.RoundToInt(Mathf.Lerp(minValue, maxValue, t));
            maxValue = Mathf.RoundToInt(Mathf.Lerp(maxValue, maxPossibleStylePoints,t)); 
            return (minValue, maxValue);
        }
        List<IMissionRequirement> CreateRandomRequirements(int requirementAmount){
            var missionRequirements = new List<IMissionRequirement>();
            var requirementAmountLeft = requirementAmount;
            while (requirementAmountLeft >= 1){
                var requirementValue = Random.Range(1, requirementAmountLeft+1);
                
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