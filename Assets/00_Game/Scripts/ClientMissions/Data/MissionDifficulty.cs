using UnityEngine;

namespace ClientMissions.Data {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClientRequestData/MissionData")]
    public class MissionDifficulty : ScriptableObject {
        [SerializeField] int minimumStylePoints;
        [SerializeField] int maximumStylePoints;
        [SerializeField] int maxCurrencyReward;
        [SerializeField] int minCurrencyReward;
        [SerializeField] int maxFollowersReward;
        [SerializeField] int minFollowersReward;
        [SerializeField] int numberOfRequirements;
        public int MinimumStylePoints => minimumStylePoints;
        public int MaximumStylePoints => maximumStylePoints;
        public int MaxCurrencyReward => maxCurrencyReward;

        public int MINCurrencyReward => minCurrencyReward;

        public int MAXFollowersReward => maxFollowersReward;

        public int MINFollowersReward => minFollowersReward;
        public int NumberOfRequirements => numberOfRequirements;
    }
}