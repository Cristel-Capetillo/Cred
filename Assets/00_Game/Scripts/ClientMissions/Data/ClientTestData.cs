using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/Mission/ClientData")]
    public class ClientTestData : ScriptableObject{
        //TODO: Texture and 3d model addressable references.
        [SerializeField, TextArea] List<string> startDialog = new List<string>();
        [SerializeField, TextArea] List<string> missionInfoDialog = new List<string>();

        public List<string> StartDialog => startDialog;

        public List<string> MissionInfoDialog => missionInfoDialog;
    }
}