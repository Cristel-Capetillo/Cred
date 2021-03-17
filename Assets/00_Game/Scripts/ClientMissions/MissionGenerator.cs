using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace Club {
    public class MissionGenerator : MonoBehaviour {

        [SerializeField] List<MissionDifficulty> difficulties;
        [SerializeField] List<ColorData> colors;
        [SerializeField] List<Rarity> rarities;
        [SerializeField] List<ClothingType> clothingTypes;

        [Header("Follower ranges for missions (easy = element 0...)")] [SerializeField]
        List<DifficultyRange> difficultyRange;
        
        MissionDifficulty GetDifficultyFromFollowers(int followers) {
            return new MissionDifficulty();
        }

        public static MissionData CreateMissionData() {
            // Select difficulty ( random difficulty, based on followers:
            // 0 followers tutorial missions
            // less than x followers = easy missions
            // greater than x followers but less than x followers = 2 easy 1 medium 
            // etc etc....
            // then select requirements depending on what difficulty was chosen
            // then recalculate style points from difficulty
             return new MissionData(null,null,null);
        }
        
    }
    [System.Serializable]
    public class DifficultyRange{
        [SerializeField] int min;
        [SerializeField] int max;
        public int Min => min;
        public int Max => max;
    }
}