using System.Threading.Tasks;
using Firebase.Database;

namespace Cred.Scripts.SaveSystem{
    public class FireBaseSaveHandler : ISaveHandler{
        
        FirebaseDatabase fbDatabase;
        string saveID;
        const string UrlToolSite = "https://cred-2528e-default-rtdb.firebaseio.com/";
        public void Authenticate(string saveID){
            fbDatabase = FirebaseDatabase.GetInstance(UrlToolSite);
            this.saveID = UserData.UserID + "/" + saveID;
        }
        public async void Save(object saveObj){
            await fbDatabase.GetReference(saveID).SetValueAsync(saveObj.ToString());
        }
        public async Task<object> Load(string loadID) {
            if (!await CheckExisting()) return null;
            var tmp = fbDatabase.GetReference(saveID).GetValueAsync().Result;
            return tmp.Value;
        }

        async Task<bool> CheckExisting() {
            var existing = await fbDatabase.GetReference(saveID).GetValueAsync();
            return existing.Exists;
        }
    }
}