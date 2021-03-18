using UnityEngine;

namespace ClientMissions.ClubMissions {
    [CreateAssetMenu]
    public class ClubData : ScriptableObject {
        [SerializeField] float stylePointsRequired;
        [SerializeField] int coinsReward;
        
        public int CalculateReward(int equippedStylePoints) {
            var percentage = Mathf.Clamp(equippedStylePoints / stylePointsRequired, 0, 1);
            return Mathf.RoundToInt(coinsReward * percentage);
        }
    }
}
