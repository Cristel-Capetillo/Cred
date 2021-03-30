using System.Collections.Generic;

namespace Clothing.Inventory {
    public class EventSpawnPredefinedWearables {
        public readonly bool isFirstSave;

        public EventSpawnPredefinedWearables(bool isFirstSave) {
            this.isFirstSave = isFirstSave;
        }
    }
}