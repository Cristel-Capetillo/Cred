using System.Collections.Generic;
using ClientMissions.Data;
using UnityEngine;

namespace ClientMissions.Helpers{
    public struct SemiRandom{
        public static int RandomIndex(int listCount){
            return Random.Range(0, listCount);
        }
        public static int AddColorVariation(List<ColorData> colorDataList){
            var colorIndex = Random.Range(0, colorDataList.Count);
            colorDataList.RemoveAt(colorIndex);
            return colorIndex;
        }
        public static int NumberGenerator(int requirementAmountLeft){
            var requirementValue = 1;
            if (requirementAmountLeft > 2)
                requirementValue = Random.Range(2, requirementAmountLeft + 1);
            return requirementValue;
        }
        public static int CycleListIndex(int currentIndex, int listCount){
            if (currentIndex < listCount - 1){
                return currentIndex + 1;
            }
            return 0;
        }
    }
}