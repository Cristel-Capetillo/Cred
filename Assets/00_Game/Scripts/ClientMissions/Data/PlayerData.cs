using UnityEngine;

namespace ClientMissions.Data {
    [System.Serializable]
    public class PlayerData {
        [SerializeField] int currentMissionIndex;
        [SerializeField] int followers;
        [SerializeField] int testCoins;
        public int Followers => followers;
        public int CurrentMissionIndex => currentMissionIndex;

        public int TestCoins => testCoins;
    }
}