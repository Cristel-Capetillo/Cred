using System;
using UnityEngine;
using Utilities;

namespace Club.ClubMissions {
    public class EnterClub : MonoBehaviour{
        EquippedStylePoints equippedStylePoints;
        [SerializeField] ClientMissions.ClubMissions.ClubData clubData;
        
            public void Start() {
            equippedStylePoints = FindObjectOfType<EquippedStylePoints>();
            EventBroker.Instance().SendMessage();
            }

            void EnterClubReward() {
                var tmp = clubData.CalculateReward(equippedStylePoints.CurrentStylePoints);
                Debug.Log("your reward is " + tmp);
        }
    }
}