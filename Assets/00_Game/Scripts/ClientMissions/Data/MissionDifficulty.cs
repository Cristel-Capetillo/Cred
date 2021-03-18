using UnityEngine;

namespace ClientMissions.Data {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClientRequestData/MissionData")]
    public class MissionDifficulty : ScriptableObject {
        [SerializeField] int minimumStylePoints;
        [SerializeField] int maximumStylePoints;
        [SerializeField] int maxReward;
        [SerializeField] int numberOfRequirements;
        public int MinimumStylePoints => minimumStylePoints;
        public int MaximumStylePoints => maximumStylePoints;
        public int MaxReward => maxReward;
        public int NumberOfRequirements => numberOfRequirements;
    }
}