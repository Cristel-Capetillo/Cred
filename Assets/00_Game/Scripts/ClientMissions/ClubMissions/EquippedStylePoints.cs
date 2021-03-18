using System;
using System.Collections.Generic;
using ClientMissions.ClubMissions;
using Clothing;
using UnityEngine;
using Utilities;

namespace Club.ClubMissions {
    public class EquippedStylePoints : MonoBehaviour{
        public int CurrentStylePoints { get; set; }
        [SerializeField] ClothingType[] clothingTypes;
        Dictionary<ClothingType, int> Dictionary = new Dictionary<ClothingType, int>();

        //TODO: Get the already worn clothes StylePoints when you start the game
        
        public void Start() {
            EventBroker.Instance().SubscribeMessage<EventWearableStylePoints>(UpdateStylePoints);
            foreach (var x in clothingTypes) {
                Dictionary[x] = 0;
            }
        }

        public void UpdateStylePoints(EventWearableStylePoints eventWearableStylePoints) {
            CurrentStylePoints = 0;
            Dictionary[eventWearableStylePoints.value.ClothingType] = eventWearableStylePoints.value.StylePoints;
            foreach (var y in Dictionary) {
                CurrentStylePoints += y.Value;
            }
            EventBroker.Instance().SendMessage(new EventUpdateStylePoints(CurrentStylePoints));
        }
        
    }
}