using UnityEngine;

namespace Cred.Scripts.Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject{
        [SerializeField] Rarity rarity;
        [SerializeField] ClothingType clothingType;
        [SerializeField] Texture texture; //TODO: Inventory icon

        public Texture Texture => texture;
        public Rarity Rarity => rarity;
        public ClothingType ClothingType => clothingType;
        
    }
}