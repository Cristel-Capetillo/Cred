using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClientMissions.Data{
    [CreateAssetMenu(fileName = "ScriptableObject/Mission/ClientData")]
    public class ClientData : ScriptableObject{
        [SerializeField] Sprite portrait;
        [SerializeField] List<ClientDialogData> clientDialogData;

        public ReadOnlyCollection<ClientDialogData> ClientDialogData => clientDialogData.AsReadOnly();
        public Sprite Portrait => portrait;
    }
}