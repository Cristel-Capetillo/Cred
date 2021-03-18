using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace Club {
    public class MissionGenerator : MonoBehaviour {
        
        [SerializeField] MissionGeneratorData generatorData;
        [SerializeField] int testFollowers = 0;
        int currentCycleIndex;
        readonly Dictionary<int, List<int>> missionCycleCombinedList = new Dictionary<int, List<int>>();
        
        void Start(){
            AddMissionCyclesToDictionary(new List<List<int>>{generatorData.EasyModeMissionCycle, generatorData.MediumModeMissionCycle, generatorData.HardModeMissionCycle});
        }
        public MissionData CreateMissionData() {
            //TODO: Check if followers is <= 0 then pick tutorial!
            var missionDifficulty = PickDifficultyMission();
            CycleIndex();//TODO: Save/load currentCycleIndex from firebase!
            return new MissionData(missionDifficulty,null,null);
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