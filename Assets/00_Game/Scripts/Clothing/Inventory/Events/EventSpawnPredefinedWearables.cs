using System.Collections.Generic;

namespace Clothing.Inventory {
    public class EventSpawnPredefinedWearables {
        public readonly Dictionary<string, CombinedWearables> wearables;
        public readonly bool isFirstSave;

        public EventSpawnPredefinedWearables(Dictionary<string, CombinedWearables> wearables, bool isFirstSave) {
            this.wearables = wearables;
            this.isFirstSave = isFirstSave;
        }
    }
}