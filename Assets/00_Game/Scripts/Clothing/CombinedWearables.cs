using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing {
    public class CombinedWearables : MonoBehaviour {
        public List<Wearable> wearable;
        public ClothingType clothingType;
        public Rarity rarity;
        public int stylePoints;

        public bool isPredefined = true;

        public int Amount { get; set; }

        void Start() {
            transform.localScale = Vector3.one;
        }

        public override string ToString() {
            var uID = "";
            foreach (var wearable1 in wearable) {
                uID += wearable1.ClothingType.name;
                uID += wearable1.colorData.name;
            }

            return uID + rarity.name + clothingType.name + stylePoints;
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