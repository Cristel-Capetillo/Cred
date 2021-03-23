using System.Collections.Generic;
using Clothing;
using UnityEngine;
using Utilities;

namespace ClientMissions.ClubMissions {
    public class EquippedStylePoints : MonoBehaviour{
        public int CurrentStylePoints { get; private set; }
        readonly Dictionary<BodyPart, int> dictionary = new Dictionary<BodyPart, int>();

        public void Awake() {
            EventBroker.Instance().SubscribeMessage<EventWearableStylePoints>(UpdateStylePoints);
        }

        void UpdateStylePoints(EventWearableStylePoints eventWearableStylePoints) {
            CurrentStylePoints = 0;
            dictionary[eventWearableStylePoints.Value.BodyPart] = eventWearableStylePoints.Value.StylePoints;
            foreach (var y in dictionary) {
                CurrentStylePoints += y.Value;
            }
            EventBroker.Instance().SendMessage(new EventUpdateStylePoints(CurrentStylePoints));
        }
    }
}