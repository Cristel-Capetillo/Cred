using System.Collections.Generic;

namespace Clothing.Inventory {
    public class EventSpawnPredefinedWearables {
        public readonly bool isFirstSave;
        public readonly Dictionary<string, object> inventory;

        public EventSpawnPredefinedWearables(Dictionary<string, object> inventory, bool isFirstSave) {
            this.isFirstSave = isFirstSave;
            this.inventory = inventory;
        }
    }
}