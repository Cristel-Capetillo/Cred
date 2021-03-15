using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace SaveSystem {
    public class JSONSaveHandler : ISaveHandler {
        
        const string SaveFilePath = "/Cred.";
        
        string SaveFileName = "";

        StreamWriter streamWriter;
        StreamReader streamReader;
        
        public void Authenticate(string saveID) {
            SaveFileName = GetFilePath(Path.Combine(SaveFilePath, Path.Combine(saveID, ".cred")));
            
        }

        public void Save(object saveObj) {
            using (streamWriter = new StreamWriter(SaveFileName)) {
                streamWriter.Write(saveObj.ToString());
            }
        }

        public Task<T> Load<T>(string loadID) {
            return default;
        }

        public Task<object> Load(string loadID) {
            using (streamReader = new StreamReader(SaveFileName)) {
                return new Task<object>(() => streamReader.ReadLine());
            }
        }
        
        string GetFilePath(string fileName)
            => Application.persistentDataPath + fileName;
    }
    //%AppData%/saveID+SaveFileName
}