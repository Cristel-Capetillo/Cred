using UnityEngine;

namespace ClientMissions.ClubMissions {
    [CreateAssetMenu]
    public class ClubData : ScriptableObject {
        [SerializeField] int stylePointsRequired;
        [SerializeField] int coinsReward;

        public int StylePointsRequired => stylePointsRequired;

        public int CalculateReward(int equippedStylePoints) {
            var percentage = Mathf.Clamp((float)equippedStylePoints / stylePointsRequired, 0, 1);
            return Mathf.RoundToInt(coinsReward * percentage);
        }
    }
}
