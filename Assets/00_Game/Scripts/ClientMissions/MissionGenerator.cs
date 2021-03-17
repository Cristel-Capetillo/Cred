using System;
using System.Collections.Generic;
using Clothing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Club {
    public class MissionGenerator : MonoBehaviour {

        [SerializeField] List<MissionDifficulty> difficulties;
        [SerializeField] List<ColorData> colors;
        [SerializeField] List<Rarity> rarities;
        [SerializeField] List<ClothingType> clothingTypes;

        [Header("Follower ranges for missions (easy = element 0...)")] [SerializeField]
        List<DifficultyRange> difficultyRange;

        public int followers;
        public int difficultyAdjuster;
        MissionDifficulty GetDifficultyFromFollowers(int followers) {
            return new MissionDifficulty();
        }

        void Start() {
            /*
             * Vad vi vill har är:
             * OM followers är högre än A och mindre än B så ska missions vara EASY
             * OM followers är högre än C och mindre än D så ska missions vara MEDIUM
             * Om followers är högre än E och mindre än F så ska missions vara HARD
             * Dessa värden A,B,C,D,E,F ska designers sätta, och dessa kan överlappa så att man kan få:
             * 1 easy + 2 medium etc..
             */
            
            
            var tmp = followers / difficultyAdjuster;
            var min = 101 - tmp;
            var max = min + 20;
            print(Random.Range(min,max) + " Random");
            print(min + " min");
            print(max + " max");
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