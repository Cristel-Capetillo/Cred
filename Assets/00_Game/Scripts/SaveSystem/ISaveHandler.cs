using System.Threading.Tasks;

namespace SaveSystem {
    public interface ISaveHandler {
        public void Authenticate(string saveID);
        public void Save(object saveObj);
        public Task<T> Load<T>(string loadID);
    }
}