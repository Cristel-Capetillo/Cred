using System.Threading.Tasks;

namespace SaveSystem {
    public interface ISaveHandler {
        public void Authenticate(string saveID);
        public void Save<T>(T saveObj);
        public Task<T> Load<T>(string loadID);
    }
}