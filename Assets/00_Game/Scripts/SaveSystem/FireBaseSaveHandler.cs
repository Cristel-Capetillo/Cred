using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace SaveSystem {
    public class FireBaseSaveHandler : ISaveHandler {
        FirebaseDatabase fbDatabase;
        FirebaseAuth user;
        string saveID;
        const string UrlToolSite = "https://cred-2528e-default-rtdb.firebaseio.com/";

        public void Authenticate(string saveID) {
            fbDatabase = FirebaseDatabase.GetInstance(UrlToolSite);

            user = FirebaseAuth.DefaultInstance;
            this.saveID = /*user.CurrentUser.UserId*/ "dasfkjhl"  + "/" + saveID;
        }

        public async void Save<T>(T saveObj) {
            await fbDatabase.GetReference(saveID).SetValueAsync(saveObj);
        }

        public async Task<T> Load<T>(string loadID) {
            var checkFile = await CheckExisting();

            if (!checkFile) return default;
            var tmp = await fbDatabase.GetReference(saveID).GetValueAsync();
            Debug.Log($"[FireBaseSaveHandler_Load]\nLoading of type: {tmp.Value.GetType()} from: {loadID}");
            return (T) tmp.Value;
        }

        async Task<bool> CheckExisting() {
            var existing = await fbDatabase.GetReference(saveID).GetValueAsync();
            return existing.Exists;
        }
    }
}