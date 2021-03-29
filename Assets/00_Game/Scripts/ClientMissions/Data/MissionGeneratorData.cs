using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [SerializeField] List<ClientData> clientData = new List<ClientData>();
        
        
        public ReadOnlyCollection<MissionDifficulty> MissionDifficulties => missionDifficulties.AsReadOnly();
        public ReadOnlyCollection<int> EasyModeMissionCycle => easyModeMissionCycle.AsReadOnly();
        public ReadOnlyCollection<int> MediumModeMissionCycle => mediumModeMissionCycle.AsReadOnly();
        public ReadOnlyCollection<int> HardModeMissionCycle => hardModeMissionCycle.AsReadOnly();
        public ReadOnlyCollection<ColorData> Colors => colors.AsReadOnly();
        public ReadOnlyCollection<Rarity> Rarities => rarities.AsReadOnly();
        public ReadOnlyCollection<ClothingType> ClothingTypes => clothingTypes.AsReadOnly();
        public ReadOnlyCollection<ClientData> ClientData => clientData.AsReadOnly();
        public int EasyModeEndValue => easyModeEndValue;
        public int HardModeStartValue => hardModeStartValue;
        
        
    }
}