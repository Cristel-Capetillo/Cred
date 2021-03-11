using System.Threading.Tasks;
using UnityEngine;

namespace Cred.Scripts.SaveSystem {
    public class PlayerPrefsLocalSave : ISaveHandler {
        readonly string currentUserID;

        public PlayerPrefsLocalSave(string currentUserID) {
            this.currentUserID = currentUserID;
            Authenticate("not working rn");
        }

        public void Authenticate(string saveID) {
            Debug.Log("Totally authenticated right now with user : "+saveID);
        }

        public void Save(string saveID, object saveObj) {
            PlayerPrefs.SetString(saveID, saveObj.ToString());
        }

        public Task<object> Load(string loadID) {
            return Task.Run(() => PlayerPrefs.GetString(loadID, "no value stored") as object);
        }
    }
}