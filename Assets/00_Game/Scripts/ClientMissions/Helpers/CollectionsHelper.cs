using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Helpers{
    public struct CollectionsHelper{
        public static List<int> CreateListOfIndexes(int targetListCount){
            var listOfIndexes = new List<int>();
            for (var i = 0; i < targetListCount; i++){
                listOfIndexes.Add(i);
            }
            return listOfIndexes;
        }
        public static int CycleListIndex(int currentIndex, int listCount){
            if (currentIndex < listCount - 1){
                return currentIndex + 1;
            }
            return 0;
        }
        public static Dictionary<int, List<int>> CombineListsToDictionary(IReadOnlyList<List<int>> difficultyMissionLists){
            var dictionary = new Dictionary<int, List<int>>();
            for (var i = 0; i < difficultyMissionLists.Count; i++){
                dictionary.Add(i,difficultyMissionLists[i]);
            }
            return dictionary;
        }
        public static int GetRandomNonRepeatingIndexFromList(List<int> listOfIndexes){
            var randomListIndex = Random.Range(0, listOfIndexes.Count);
            var index = listOfIndexes[randomListIndex];
            listOfIndexes.RemoveAt(randomListIndex);
            return index;
        }
    }
}