using ClientMissions.ClubMissions;
using ClientMissions.MissionMessages;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Clothing {
    public class StylePointsRequiredUI : MonoBehaviour{
        [SerializeField] string customText;
        [SerializeField] Text stylePointText;
        int currentStylePoints;
        int clubRequiredStylePoints;

        void UpdateStylePointsText(EventUpdateStylePoints eventUpdateStylePoints) {
            currentStylePoints = eventUpdateStylePoints.CurrentStylePoints;
            stylePointText.text = customText + currentStylePoints + "/" + clubRequiredStylePoints;
        }

        void UpdateClubRequiredText(EventUpdateRequiredStylePoints clubStylePoints) {
            clubRequiredStylePoints = clubStylePoints.Points;
            stylePointText.text = customText + currentStylePoints + "/" + clubRequiredStylePoints;
        }
        
        public void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdateStylePoints>(UpdateStylePointsText);
            EventBroker.Instance().SubscribeMessage<EventUpdateRequiredStylePoints>(UpdateClubRequiredText);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateStylePoints>(UpdateStylePointsText);
            EventBroker.Instance().UnsubscribeMessage<EventUpdateRequiredStylePoints>(UpdateClubRequiredText);
        }
    }
}