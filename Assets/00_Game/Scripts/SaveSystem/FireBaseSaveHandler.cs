using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

namespace SaveSystem {
    public class FireBaseSaveHandler : ISaveHandler {
        FirebaseDatabase fbDatabase;
        string saveID;
        const string UrlToolSite = "https://cred-2528e-default-rtdb.firebaseio.com/";

        public void Authenticate(string saveID) {
            fbDatabase = FirebaseDatabase.GetInstance(UrlToolSite);
            
            this.saveID = "JsuJjn1YHZdrgTGvz5hsYs3HFkl1" + "/" + saveID;
        }

        public async void Save<T>(T saveObj) {
            await fbDatabase.GetReference(saveID).SetValueAsync(saveObj);
        }

        public async Task<T> Load<T>(string loadID) {
            var checkFile = await CheckExisting();

            if (!checkFile) return default;
            var tmp = await fbDatabase.GetReference(saveID).GetValueAsync();
            return (T) tmp.Value;
        }

        async Task<bool> CheckExisting() {
            var existing = await fbDatabase.GetReference(saveID).GetValueAsync();
            return existing.Exists;
        }
    }
}