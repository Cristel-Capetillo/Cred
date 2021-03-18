using System;
using UnityEngine;
using Utilities;

namespace ClientMissions.ClubMissions {
    public class EnterClub : MonoBehaviour{
        EquippedStylePoints equippedStylePoints;
        [SerializeField] ClubData clubData;
        
            public void Start() {
            equippedStylePoints = FindObjectOfType<EquippedStylePoints>();
            EventBroker.Instance().SendMessage(new EventUpdateRequiredStylePoints(clubData.StylePointsRequired));
            }

            public void EnterClubReward() {
                if (equippedStylePoints == null) {
                    throw new Exception("cannot find equippedstylepoints");
                    return;
                }
                Debug.Log("Enter club");
                var reward = clubData.CalculateReward(equippedStylePoints.CurrentStylePoints);
                Debug.Log("your reward is " + reward);
                EventBroker.Instance().SendMessage(new EventShowReward(reward));
            }
    }
}