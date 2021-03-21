using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/MissionData/GeneratorData")]
    public class MissionGeneratorData : ScriptableObject{
        [SerializeField] List<MissionDifficulty> missionDifficulties;
        [Header("Values based on missionDifficulties index")]
        [SerializeField,Range(0, 2)]List<int> easyModeMissionCycle = new List<int>(10);
        [SerializeField,Range(0, 2)]List<int> mediumModeMissionCycle = new List<int>(10);
        [SerializeField,Range(0, 2)]List<int> hardModeMissionCycle = new List<int>(10);
        [Header("Values based on followers:")]
        [SerializeField]int easyModeEndValue = 200;
        [SerializeField]int hardModeStartValue = 1000;
        [SerializeField] List<ColorData> colors;
        [SerializeField] List<Rarity> rarities;
        [SerializeField] List<ClothingType> clothingTypes;
        [SerializeField] List<ClientTestData> clientData = new List<ClientTestData>();
        
        
        public List<MissionDifficulty> MissionDifficulties => missionDifficulties;
        public List<int> EasyModeMissionCycle => easyModeMissionCycle;
        public List<int> MediumModeMissionCycle => mediumModeMissionCycle;
        public List<int> HardModeMissionCycle => hardModeMissionCycle;
        public List<ColorData> Colors => colors;
        public List<Rarity> Rarities => rarities;
        public List<ClothingType> ClothingTypes => clothingTypes;
        public List<ClientTestData> ClientData => clientData;
        public int EasyModeEndValue => easyModeEndValue;
        public int HardModeStartValue => hardModeStartValue;
        
        
    }
}