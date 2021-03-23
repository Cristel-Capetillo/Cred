using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace ClientMissions{
    public class MissionButtonScript : MonoBehaviour, IPointerClickHandler{

        MissionData missionData;
        [SerializeField]Text testText;

        public MissionData MissionData => missionData;

        void Start(){
            if(missionData == null)
                testText.text = "Locked";
        }

        public void Setup(MissionData missionData){
            if (missionData == null){
                this.missionData = null;
                return;
            }
            this.missionData = missionData;
            testText.text = missionData.ClientTestData.name;
        }
        public void OnPointerClick(PointerEventData eventData){
            EventBroker.Instance().SendMessage(new SelectMissionMessage(missionData));
        }
    }
}