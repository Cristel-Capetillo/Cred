using System.Collections.Generic;
using UnityEngine;

namespace Club {
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