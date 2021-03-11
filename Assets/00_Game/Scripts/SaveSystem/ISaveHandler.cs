using System.Threading.Tasks;

namespace Cred.Scripts.SaveSystem {
    public interface ISaveHandler {
        public void Authenticate(string saveID);
        public void Save(object saveObj);
        public Task<T> Load<T>(string loadID);
    }
}