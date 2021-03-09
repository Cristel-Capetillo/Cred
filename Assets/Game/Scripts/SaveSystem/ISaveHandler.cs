namespace Cred.Scripts.SaveSystem {
    public interface ISaveHandler {
        public void Authenticate();
        public void Save(string ID, object toSave);
        public object Load();
    }
}