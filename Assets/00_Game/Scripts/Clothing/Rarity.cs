using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Rarity")]
    public class Rarity : ScriptableObject{
        [SerializeField] int value;
        public int Value => value;
    }
}