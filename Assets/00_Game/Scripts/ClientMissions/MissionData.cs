using System.Collections.Generic;
using Clothing;

namespace Club {
    public class MissionData {
        public MissionDifficulty Difficulty{ get; private set; }
        public List<MissionRequirement> Requirements{ get; private set; }
        public StylePointValues StylePointValues{ get; private set;}

        public MissionData(MissionDifficulty difficulty, List<MissionRequirement> requirements, StylePointValues stylePointValues) {
            Difficulty = difficulty;
            Requirements = requirements;
            StylePointValues = stylePointValues;
        }
        
    }
    public class MissionRequirement {
        public MissionRequirement(ColorData color, Rarity rarity, ClothingType clothingType) {
            Color = color;
            Rarity = rarity;
            ClothingType = clothingType;
        }
        public ColorData Color { get; private set; }
        public Rarity Rarity { get; private set; } 
        public ClothingType ClothingType { get; private set; } 
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
