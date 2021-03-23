using System.Collections.Generic;
using ClientMissions.Data;
using Club;
using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/Wearable")]
    public class Wearable : ScriptableObject {
        [SerializeField] Rarity rarity;
        [SerializeField] ClothingType clothingType;
        [SerializeField] Texture texture; //TODO: Inventory icon
        [SerializeField] Sprite sprite;
        [SerializeField] List<ColorData> colorData = new List<ColorData>();
        int stylePoints;
        [SerializeField] int amount;

        public Texture addedUpcycleTexture;

        ////////////////////*Upcycle Wearables*///////////////////////////
        [SerializeField ]public bool isUpCycledWearable;
        
        [HideInInspector]public bool unlockedUpcycle;
        /////////////////////////////////////////////////////////////////
        public int StylePoints => stylePoints + rarity.Value;
        public int Amount => amount;
 
        public bool Unlocked() {
            return Amount > 0;
        }

        public bool HasUnlockedUpCycledWearable()
        {
            return isUpCycledWearable && unlockedUpcycle;
        }

        public List<ColorData> ColorData => colorData;
        public Texture Texture => texture;
        public Sprite Sprite => sprite;
        public Rarity Rarity => rarity;
        public ClothingType ClothingType => clothingType;
        
        public void AddStylePoint() {
            if (StylePoints < Rarity.MaxValue) {
                stylePoints++;
            }
        }

        public void SetStylePoints(int sp) {
            stylePoints = sp - rarity.Value;
        }

        public void SetAmount(int i) {
            amount = i;
        }

    }
}