using System.Collections.Generic;
using Clothing;
using UnityEngine;

namespace MysteryBox {
    [CreateAssetMenu(menuName = "ScriptableObjects/Loot Table")]
    public class LootTable : ScriptableObject {
        public List<CombinedWearables> loot;

        public CombinedWearables Reward() {
            return loot[Random.Range(0, loot.Count)];
        }
    }
}
