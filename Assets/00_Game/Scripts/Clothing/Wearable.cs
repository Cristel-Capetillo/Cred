using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject {
        [SerializeField] Rarity rarity;
        [SerializeField] ClothingType clothingType;
        [SerializeField] Texture texture; //TODO: Inventory icon
        [SerializeField] Sprite sprite;
        int _stylePoints;
        int _amountOwned;

        public int StylePoints => _stylePoints + rarity.Value;
        public int AmountOwned => _amountOwned;
        
        public Texture Texture => texture;
        public Sprite Sprite => sprite;
        public Rarity Rarity => rarity;
        public ClothingType ClothingType => clothingType;

        public void AddStylePoint() {
            if (StylePoints < Rarity.MaxValue) {
                _stylePoints++;
            }
        }

        public void SetStylePoints(int stylePoints) {
            _stylePoints = stylePoints - rarity.Value;
        }
    }
}