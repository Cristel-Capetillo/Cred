namespace Cred.Scripts.SaveSystem {
    public interface ISavable {
        public object ToBeSaved();
        public void OnLoad(object value);
    }
}