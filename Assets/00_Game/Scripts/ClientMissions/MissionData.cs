using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace Club {
    public class MissionData {
        public MissionDifficulty Difficulty{ get; private set; }
        public List<IMissionRequirement> Requirements{ get; private set; }
        public StylePointValues StylePointValues{ get; private set;}

        public MissionData(MissionDifficulty difficulty, List<IMissionRequirement> requirements, StylePointValues stylePointValues) {
            Difficulty = difficulty;
            Requirements = requirements;
            StylePointValues = stylePointValues;
        }
    }
    public class MatchColor: IMissionRequirement{
        public MatchColor(ColorData colorData) {
            ColorData = colorData;
        }
        public ColorData ColorData { get; private set; }
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData);
        }
    }
    public class MatchColorAndClothingType : IMissionRequirement{
        public MatchColorAndClothingType(ColorData colorData, ClothingType clothingType){
            ColorData = colorData;
            ClothingType = clothingType;
        }
        public ColorData ColorData { get; private set; }
        public ClothingType ClothingType{ get; private set; }
        
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) && wearable.ClothingType == ClothingType;
        }
    }

    public class MatchColorClothingTypeAndRarity : IMissionRequirement{
        public MatchColorClothingTypeAndRarity(ColorData colorData, ClothingType clothingType, Rarity rarity){
            ColorData = colorData;
            ClothingType = clothingType;
            Rarity = rarity;
        }
        public ColorData ColorData { get; private set; }
        public ClothingType ClothingType{ get; private set; }
        public Rarity Rarity { get; private set; }
        public bool PassedRequirement(Wearable wearable){
            return wearable.ColorData.Contains(ColorData) && 
                   wearable.ClothingType == ClothingType && wearable.Rarity == Rarity;
        }
    }

    public interface IMissionRequirement{
        bool PassedRequirement(Wearable wearable);
    }

    public class StylePointValues {
        public int MinStylePoints { get; private set; }
        public int MaxStylePoints { get; private set; }

        public StylePointValues(int minStylePoints, int maxStylePoints) {
            MinStylePoints = minStylePoints;
            MaxStylePoints = maxStylePoints;
        }
    }
}
