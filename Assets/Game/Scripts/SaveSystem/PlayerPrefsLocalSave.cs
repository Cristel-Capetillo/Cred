using System.Threading.Tasks;
using UnityEngine;

namespace Cred.Scripts.SaveSystem {
    public class PlayerPrefsLocalSave : ISaveHandler {
        readonly string currentUserID;

        public PlayerPrefsLocalSave(string currentUserID) {
            this.currentUserID = currentUserID;
            Authenticate();
        }

        public void Authenticate() {
            Debug.Log("Totally authenticated right now with user : "+currentUserID);
        }

        public void Save(string saveID, object saveObj) {
            PlayerPrefs.SetString(saveID, saveObj.ToString());
        }

        public async Task<object> Load(string loadID) {
            var tmp = await Task.Run(() => PlayerPrefs.GetString(loadID, "no value stored"));
            return tmp;
        }
    }
}