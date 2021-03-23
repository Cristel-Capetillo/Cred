using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClientMissions.Helpers{
    public struct Helper{
        public static List<int> CreateListOfIndexes(int targetListCount){
            var listOfIndexes = new List<int>();
            for (var i = 0; i < targetListCount; i++){
                listOfIndexes.Add(i);
            }
            return listOfIndexes;
        }
        public static int NumberGenerator(int requirementAmountLeft){
            return requirementAmountLeft < 3 ? Random.Range(1, requirementAmountLeft + 1) : 
                Random.Range(2, requirementAmountLeft + 1);
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
        public static int GetLowestNumberFromThreeNumbers(int numberOne, int numberTwo, int numberThree){
            return Mathf.Min(Mathf.Min(numberOne,numberTwo), numberThree);
        }

        public static long ToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            Debug.Log(date.Kind);
            var unixTimestamp = Convert.ToInt64((target - date).TotalSeconds);

            return unixTimestamp;
        }

        public static DateTime ToDateTime(DateTime target, long timestamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return dateTime.AddSeconds(timestamp);
        }
    }
}