using UnityEngine;

namespace Cred.Scripts.Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject{
        [SerializeField] Rarity rarity;
        [SerializeField] ClothingType clothingType;
        [SerializeField] Texture texture; //TODO: Inventory icon
        [SerializeField] Sprite sprite;

        public Texture Texture => texture;
        public Sprite Sprite => sprite;
        public Rarity Rarity => rarity;
        public ClothingType ClothingType => clothingType;
        
    }
}