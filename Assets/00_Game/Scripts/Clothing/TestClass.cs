using System.Collections.Generic;

namespace Clothing {
    public class TestClass {
        public readonly List<Wearable> wearable;
        public ClothingType clothingType;
        public Rarity rarity;
        public int stylePoints;

        public bool isPredefined = true;

        public int Amount { get; set; }

        public TestClass(List<Wearable> wearable) {
            this.wearable = wearable;
        }
        
        public override string ToString() {
            var uID = "";
            foreach (var wearable1 in wearable) {
                uID += wearable1.ClothingType.name;
                uID += wearable1.colorData.name;
            }

            return uID + rarity.name + clothingType.name + stylePoints;
        }
    }
}