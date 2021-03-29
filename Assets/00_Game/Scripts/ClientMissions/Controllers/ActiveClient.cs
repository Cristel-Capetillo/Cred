using ClientMissions.Data;
using ClientMissions.Messages;
using UnityEngine;
using Utilities;

namespace ClientMissions.Controllers{
    public class ActiveClient : MonoBehaviour{

        MissionData missionData;

        void Start(){
            DontDestroyOnLoad(this);
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
            EventBroker.Instance().SubscribeMessage<SceneChangeMessage>(OnDressupSceneStart);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
            EventBroker.Instance().UnsubscribeMessage<SceneChangeMessage>(OnDressupSceneStart);
        }
        void OnDressupSceneStart(SceneChangeMessage sceneChangeMessage){
            if(missionData == null)
                return;
            EventBroker.Instance().SendMessage(new SendActiveMissionMessage(missionData));
            print("SceneChangeMessage");
        }
        void RemoveCurrentMission(ShowRewardMessage showRewardMessage){
            new LocalPlayer().RemoveMission(missionData.SavableMissionData);
            missionData = null;
        }
        void SelectMission(ActiveMissionMessage activeMissionMessage){
            missionData = activeMissionMessage.MissionData;
            print("SendMessage");
        }
    }
}
