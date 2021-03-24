using System.Collections.Generic;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/Mission/ClientData")]
    public class ClientTestData : ScriptableObject{
        [SerializeField] Sprite portrait;
        [SerializeField] List<ClientDialogData> clientDialogData;

        public List<ClientDialogData> ClientDialogData => clientDialogData;
        public Sprite Portrait => portrait;
    }
}