using System.Threading.Tasks;

namespace Cred.Scripts.SaveSystem {
    public interface ISaveHandler {
        public void Authenticate();
        public void Save(string saveID, object saveObj);
        public Task<object> Load(string loadID);
    }
}