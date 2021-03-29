using ClientMissions.Data;
using ClientMissions.Hud;
using ClientMissions.Messages;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace ClientMissions.Controllers{
    public class ClientButton : MonoBehaviour, IPointerClickHandler{
        //TODO: Add timeLeftLogic
        [SerializeField] ClientMissionInfo clientMissionInfo;
        public MissionData MissionData{ get; private set; }

        public void Setup(MissionData missionData){
            if (missionData == null){
                Debug.LogWarning("No missionData was sent...");
                return;
            }
            clientMissionInfo.SetUp(missionData.ClientData.name, missionData.ClientData.Portrait);
            MissionData = missionData;
        }
        public void OnPointerClick(PointerEventData eventData){
            EventBroker.Instance().SendMessage(new SelectMissionMessage(MissionData));
        }
    }
}