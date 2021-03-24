using System.Collections.Generic;
using Clothing;
using UnityEngine;
using Utilities;

namespace ClientMissions.ClubMissions {
    public class EquippedStylePoints : MonoBehaviour{
        public int CurrentStylePoints { get; private set; }
        readonly Dictionary<ClothingType, int> dictionary = new Dictionary<ClothingType, int>();

        public void Awake() {
            EventBroker.Instance().SubscribeMessage<EventWearableStylePoints>(UpdateStylePoints);
        }

        void UpdateStylePoints(EventWearableStylePoints eventWearableStylePoints) {
            CurrentStylePoints = 0;
            dictionary[eventWearableStylePoints.combinedWearable.clothingType] = eventWearableStylePoints.combinedWearable.stylePoints;
            foreach (var y in dictionary) {
                CurrentStylePoints += y.Value;
            }
            EventBroker.Instance().SendMessage(new EventUpdateStylePoints(CurrentStylePoints));
        }
    }
}