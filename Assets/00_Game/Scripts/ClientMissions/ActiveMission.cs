using ClientMissions.Data;
using ClientMissions.MissionMessages;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace ClientMissions{
    public class ActiveMission : MonoBehaviour{
        public bool IsNewMission{ get; private set; }

        public MissionData ActiveMissionData{ get; private set; }

        void Start(){
            DontDestroyOnLoad(this);
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().SubscribeMessage<EventShowReward>(RemoveCurrentMission);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().UnsubscribeMessage<EventShowReward>(RemoveCurrentMission);
        }

        public void OnStartMission(){
            IsNewMission = false;
        }
        void RemoveCurrentMission(EventShowReward eventShowReward){
            new LocalPlayerTest().RemoveMission(ActiveMissionData.SavableMissionData);
            ActiveMissionData = null;
            IsNewMission = false;
        }
        void SelectMission(ActiveMissionMessage activeMissionMessage){
            ActiveMissionData = activeMissionMessage.missionData;
            IsNewMission = true;
        }
    }
}
