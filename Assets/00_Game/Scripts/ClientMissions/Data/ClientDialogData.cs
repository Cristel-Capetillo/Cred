using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/ClientDialogData")]
    public class ClientDialogData : ScriptableObject{
        [SerializeField, TextArea] List<string> dialog = new List<string>();
        [SerializeField] ClubData clubData;
        
        public ReadOnlyCollection<string> Dialog => dialog.AsReadOnly();
        public ClubData ClubData => clubData;
    }
}