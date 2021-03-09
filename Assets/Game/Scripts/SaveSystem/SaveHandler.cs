namespace Cred.Scripts.SaveSystem {
    public class SaveHandler {

        ISaveHandler backEndSaveSystem;

        public SaveHandler(ISaveHandler backEndSaveSystem) {
            this.backEndSaveSystem = backEndSaveSystem;
            this.backEndSaveSystem.Authenticate();
        }

        public void Save(string ID, ISavable savable) {
            backEndSaveSystem.Save(ID, savable.ToBeSaved());
        }
    }
}