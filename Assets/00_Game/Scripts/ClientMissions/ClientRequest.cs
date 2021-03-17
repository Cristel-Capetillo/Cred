using Clothing;
using UnityEngine;

namespace Club {
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission")]
    public class ClientRequest : ScriptableObject {
   public int maxReward = 500; // this is used to have a base to calculate the reward from, is the max reward user can get
        float maxStylePoints = 50; // maximum style points you can ever get in game
        public int stylePoints;
        public Wearable[] mustHaveTypes;

        // calculate how many style points this mission requires
        // Based on a set amount of maximum followers that can be reached.
        public int CalculateStylePoints(int followers, int maxFollowers) {
            var t = Mathf.InverseLerp(0, maxFollowers, followers);
            return Mathf.RoundToInt(Mathf.Lerp(1, maxStylePoints,t)); 
        }

        // calculate the percentage of the current selected style.
        public float CalculateStylePercentage(int stylePoints, float currentMissionStylePoints) {
            return stylePoints / currentMissionStylePoints;
        }
        
        // calculate the total reward the user gets from that style percentage
        public int CalculateTotalReward(float stylePercentage){
            return (int) (stylePercentage * maxReward);
        }     

    }
}
