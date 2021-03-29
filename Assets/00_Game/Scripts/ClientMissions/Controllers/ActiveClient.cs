using ClientMissions.Data;
using ClientMissions.Messages;
using UnityEngine;
using Utilities;

namespace ClientMissions.Controllers{
    public class ActiveClient : MonoBehaviour{
        public bool IsNewMission{ get; private set; }

        public MissionData ActiveMissionData{ get; private set; }

        void Start(){
            DontDestroyOnLoad(this);
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
        }

        public void OnStartMission(){
            IsNewMission = false;
        }
        void RemoveCurrentMission(ShowRewardMessage showRewardMessage){
            new LocalMissions().RemoveMission(ActiveMissionData.SavableMissionData);
            ActiveMissionData = null;
            IsNewMission = true;
        }
        void SelectMission(ActiveMissionMessage activeMissionMessage){
            ActiveMissionData = activeMissionMessage.MissionData;
            IsNewMission = true;
        }
    }
}
