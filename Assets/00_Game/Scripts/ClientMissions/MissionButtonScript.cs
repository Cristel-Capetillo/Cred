using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions{
    public class MissionButtonScript : MonoBehaviour, IPointerClickHandler{

        MissionData missionData;
        [SerializeField]Image clientPortrait;

        public MissionData MissionData => missionData;

        public void Setup(MissionData missionData){
            if (missionData == null){
                Debug.LogWarning("No missionData was sent...");
                return;
            }
            this.missionData = missionData;
            clientPortrait.sprite = missionData.ClientTestData.Portrait;
        }
        public void OnPointerClick(PointerEventData eventData){
            EventBroker.Instance().SendMessage(new SelectMissionMessage(missionData));
        }
    }
}