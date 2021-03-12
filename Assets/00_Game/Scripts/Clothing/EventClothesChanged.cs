using Cred.Scripts.Clothing;
using UnityEngine;

namespace _00_Game.Scripts.Clothing {
    public class EventClothesChanged
    {
        public readonly Texture textureChanged;
        public readonly string bodyPart;

        public EventClothesChanged(Wearable wearable) {
            textureChanged = wearable.Texture;
            bodyPart = wearable.ClothingType.name;
        }
        
    }
}