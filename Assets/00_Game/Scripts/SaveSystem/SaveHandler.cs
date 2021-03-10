using UnityEngine;

namespace Cred.Scripts.SaveSystem {
    public class SaveHandler {

        string objectID;
        ISaveHandler backEndSaveSystem;

        public SaveHandler(string objectID) {
            this.objectID = objectID;
            backEndSaveSystem = new PlayerPrefsLocalSave(objectID);
            this.backEndSaveSystem.Authenticate("test");
        }

        public void Save(ISavable savable) {
            backEndSaveSystem.Save(objectID, savable.ToBeSaved());
        }

        public async void Load(ISavable savable) {
            var tmp = await backEndSaveSystem.Load(objectID);
            savable.OnLoad(tmp);
            Debug.Log("Object loaded from backEndSaveSystem: "+objectID);
        }
    }
}