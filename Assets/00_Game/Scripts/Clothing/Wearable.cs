using ClientMissions.Data;
using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject {
        [SerializeField] BodyPart clothingType;
        [SerializeField] Texture texture; //TODO: Inventory icon
        [SerializeField] Sprite sprite;
        public ColorData colorData;

        public Texture Texture => texture;
        public Sprite Sprite => sprite;
        public BodyPart ClothingType => clothingType;


        public override string ToString() {
            return name;
        }
    }
}