using UnityEngine;
using Utilities;

namespace SaveSystem {
    public class SaveHandler {
        string objectID;
        ISaveHandler backEndSaveSystem;

        public SaveHandler(string objectID) {
            this.objectID = objectID;
            backEndSaveSystem = new FireBaseSaveHandler();
            backEndSaveSystem.Authenticate(this.objectID);
        }

        public void Save<T>(ISavable<T> savable) {
            backEndSaveSystem.Save(savable.ToBeSaved());
        }

        public async void Load<T>(ISavable<T> savable) {
            var tmp = await backEndSaveSystem.Load<T>(objectID);
            savable.OnLoad(tmp);
            EventBroker.Instance().SendMessage(new EventAfterLoad(true));
            //Debug.Log("Object loaded from backEndSaveSystem: " + objectID);
        }
    }
}