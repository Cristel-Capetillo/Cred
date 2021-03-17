using System.Collections.Generic;
using Clothing;

namespace Club {
    public class MissionData {
        MissionDifficulty difficulty;
        List<MissionRequirement> requirements;
        StylePointValues stylePointValues;

        public MissionData(MissionDifficulty difficulty, List<MissionRequirement> requirements, StylePointValues stylePointValues) {
            this.difficulty = difficulty;
            this.requirements = requirements;
            this.stylePointValues = stylePointValues;
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
