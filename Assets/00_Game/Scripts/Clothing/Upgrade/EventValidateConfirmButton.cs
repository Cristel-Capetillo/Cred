namespace Clothing.Upgrade {
    public class EventValidateConfirmButton {
        public readonly bool validateButton;
        public readonly CombinedWearables combinedWearables;
        
        public EventValidateConfirmButton(bool validateButton) {
            this.validateButton = validateButton;
        }
        
        public EventValidateConfirmButton(bool validateButton, CombinedWearables combinedWearables) {
            this.validateButton = validateButton;
            this.combinedWearables = combinedWearables;
        }
    }
}