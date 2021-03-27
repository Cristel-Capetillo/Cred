using System.Collections.Generic;

namespace Clothing.Inventory {
    [System.Serializable]
    public class InventoryData {
        public List<CombinedWearables> predefinedCombinations;
        public CombinedWearables[] firstSave;

        public Rarity[] rarity;
        public ClothingType[] clothingType;
        public Wearable[] wearables;

        public Dictionary<Rarity, Rarity> allRarities = new Dictionary<Rarity, Rarity>();
        public Dictionary<ClothingType, ClothingType> allClothingTypes = new Dictionary<ClothingType, ClothingType>();

        public const string WearableCount = "Wearable Count";
        public const string Rarity = "Rarity";
        public const string ClothingType = "ClothingType";
        public const string Amount = "Amount";
        public const string StylePoints = "StylePoints";
        public const string IsPredefined = "IsPredefined";

        public void Setup() {
            foreach (var r in rarity) {
                allRarities[r] = r;
            }

            foreach (var c in clothingType) {
                allClothingTypes[c] = c;
            }
        }

        public Dictionary<string, object> StatList(CombinedWearables combinedWearables) {
            var statsDictionary = new Dictionary<string, object>();

            var sortIndex = 0;
            foreach (var data in combinedWearables.wearable) {
                statsDictionary.Add(data.ToString() + sortIndex, "");
                sortIndex++;
            }

            statsDictionary[WearableCount] = combinedWearables.wearable.Count;
            statsDictionary[Rarity] = combinedWearables.rarity.name;
            statsDictionary[ClothingType] = combinedWearables.clothingType.name;
            statsDictionary[StylePoints] = combinedWearables.stylePoints;
            statsDictionary[Amount] = 0;
            statsDictionary[IsPredefined] = combinedWearables.isPredefined;
            return statsDictionary;
        }
    }
}