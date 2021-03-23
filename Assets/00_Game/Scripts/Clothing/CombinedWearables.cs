using System.Collections.Generic;
using UnityEngine;

namespace Clothing {
    [CreateAssetMenu]
    public class CombinedWearables : ScriptableObject {
        public List<Wearable> wearable;
        
        public Rarity rarity;
        public int stylePoints;

    }
}