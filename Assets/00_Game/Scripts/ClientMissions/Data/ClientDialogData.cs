using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/ClientDialogData")]
    public class ClientDialogData : ScriptableObject{
        [SerializeField, TextArea] List<string> dialog = new List<string>();
        [SerializeField] ClubData clubData;
        
        public List<string> Dialog => dialog;
        public ClubData ClubData => clubData;
    }
}