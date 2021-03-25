using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using Utilities;

namespace ClientMissions{
    public class ActiveMission : MonoBehaviour{
        public MissionData ActiveMissionData{ get; private set; }
        
        void Start(){
            DontDestroyOnLoad(this);
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(SelectMission);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(SelectMission);
        }
        public void RemoveCurrentMission(){
            ActiveMissionData = null;
        }
        void SelectMission(ActiveMissionMessage activeMissionMessage){
            ActiveMissionData = activeMissionMessage.missionData;
        }
    }
}
