using System;
using ClientMissions.ClubMissions;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.Clothing {
    public class StylePointsRequiredUI : MonoBehaviour{
        [SerializeField] string customText;
        [SerializeField] Text stylePointText;

        public void UpdateStylePointsText(EventUpdateStylePoints eventUpdateStylePoints) {
            stylePointText.text = customText + eventUpdateStylePoints.currentStylePoints;
        }
        
        public void Start() {
            EventBroker.Instance().SubscribeMessage<EventUpdateStylePoints>(UpdateStylePointsText);
        }
    }
}