using System;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace Cred.Scripts.SaveSystem {
    public class JSONSaveHandler : ISaveHandler {
        
        const string SaveFilePath = "/Cred.";
        
        string SaveFileName = "";

        StreamWriter streamWriter;
        StreamReader streamReader;
        
        public void Authenticate(string saveID) {
            SaveFileName = GetFilePath(Path.Combine(SaveFilePath, Path.Combine(saveID, ".cred")));
            
        }

        public void Save(string saveID, object saveObj) {
            using (streamWriter = new StreamWriter(SaveFileName)) {
                streamWriter.Write(saveObj.ToString());
            }
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