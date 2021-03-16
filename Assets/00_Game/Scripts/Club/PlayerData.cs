using UnityEngine;

namespace Club {
    [System.Serializable]
    public class PlayerData {
        [SerializeField] int currentMissionIndex;
        [SerializeField] int followers;
        public int Followers => followers;
        public int CurrentMissionIndex => currentMissionIndex;
    }
}