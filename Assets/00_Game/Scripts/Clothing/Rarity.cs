using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Rarity")]
    public class Rarity : ScriptableObject{
        [SerializeField] int value;
        [SerializeField] int maxValue;
        public int Value => value;
        public int MaxValue => maxValue;
    }
}