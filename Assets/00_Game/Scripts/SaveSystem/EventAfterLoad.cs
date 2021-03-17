namespace SaveSystem {
    public class EventAfterLoad {
        public readonly bool shouldRunAfterLoad;

        public EventAfterLoad(bool shouldRunAfterLoad) {
            this.shouldRunAfterLoad = shouldRunAfterLoad;
        }
    }
}