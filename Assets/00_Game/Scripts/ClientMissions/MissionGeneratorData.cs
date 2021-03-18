using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace Club{
    [CreateAssetMenu(fileName = "ScriptableObject/MissionData/GeneratorData")]
    public class MissionGeneratorData : ScriptableObject{
        [SerializeField] List<MissionDifficulty> missionDifficulties;
        [SerializeField] List<ColorData> colors;
        [SerializeField] List<Rarity> rarities;
        [SerializeField] List<ClothingType> clothingTypes;
        [SerializeField]int easyModeEndValue = 200;
        [SerializeField]int hardModeStartValue = 1000;
        [SerializeField]List<int> easyModeMissionCycle = new List<int>(10);
        [SerializeField]List<int> mediumModeMissionCycle = new List<int>(10);
        [SerializeField]List<int> hardModeMissionCycle = new List<int>(10);

        public List<MissionDifficulty> MissionDifficulties => missionDifficulties;
        public List<ColorData> Colors => colors;
        public List<Rarity> Rarities => rarities;
        public List<ClothingType> ClothingTypes => clothingTypes;
        public int EasyModeEndValue => easyModeEndValue;
        public int HardModeStartValue => hardModeStartValue;
        public List<int> EasyModeMissionCycle => easyModeMissionCycle;
        public List<int> MediumModeMissionCycle => mediumModeMissionCycle;
        public List<int> HardModeMissionCycle => hardModeMissionCycle;
    }
}