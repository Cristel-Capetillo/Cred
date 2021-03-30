namespace Clothing.Inventory {
    public class EventBuyNotOwnedClothes {
        public CombinedWearables CombinedWearables;

        public EventBuyNotOwnedClothes(CombinedWearables combinedWearables) {
            this.CombinedWearables = combinedWearables;
        }
    }
}