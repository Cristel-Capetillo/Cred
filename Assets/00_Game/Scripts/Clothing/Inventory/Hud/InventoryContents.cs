using UnityEngine;

namespace Clothing.Inventory {
    [System.Serializable]
    public class InventoryContents {
        public ClothingType clothingType;
        public Transform basic;
        public Transform normal;
        public Transform design;
    }
}