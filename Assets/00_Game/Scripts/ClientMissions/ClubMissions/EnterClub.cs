using System;
using UnityEngine;

namespace Club.ClubMissions {
    public class EnterClub : MonoBehaviour{
        EquippedStylePoints equippedStylePoints;
        [SerializeField] ClientMissions.ClubMissions.ClubData clubData;
        
            public void Start() {
            equippedStylePoints = FindObjectOfType<EquippedStylePoints>();
            }

            public void Update() {
                if (Input.GetKeyDown(KeyCode.K)) {
                    var tmp = clubData.CalculateReward(equippedStylePoints.CurrentStylePoints);
                    Debug.Log("your reward is " + tmp);
                }
            }
    }
}