using System.Collections.Generic;
using UnityEngine;

namespace Clothing {
    public class CombinedWearables : MonoBehaviour {
        public List<Wearable> wearable;
        public ClothingType clothingType;
        public Rarity rarity;
        public int stylePoints;

        public override string ToString() {
            var uID = "";
            foreach (var wearable1 in wearable) {
                uID += wearable1.BodyPart;
                uID += wearable1.colorData;
            }

            return uID + rarity + clothingType;
        }

        public void AddStylePoint() {
            if (stylePoints < rarity.MaxValue) {
                stylePoints++;
            }
        }

        public void SetStylePoints(int sp) {
            stylePoints = sp - rarity.Value;
        }
    }
}