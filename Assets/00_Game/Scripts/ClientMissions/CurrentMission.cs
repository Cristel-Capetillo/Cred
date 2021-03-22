using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using Utilities;

namespace ClientMissions{
    public class CurrentMission : MonoBehaviour{
        public MissionData CurrentMissionData{ get; private set; }
        
        void Start(){
            DontDestroyOnLoad(this);
            EventBroker.Instance().SubscribeMessage<CurrentMissionMessage>(SelectMission);
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<CurrentMissionMessage>(SelectMission);
        }

        public void RemoveCurrentMission(){
            CurrentMissionData = null;
        }
        
        void SelectMission(CurrentMissionMessage currentMissionMessage){
            CurrentMissionData = currentMissionMessage.missionData;
        }
    }
}
