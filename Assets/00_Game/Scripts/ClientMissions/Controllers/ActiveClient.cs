using System.Collections.Generic;
using ClientMissions.Data;
using ClientMissions.Messages;
using Clothing;
using UnityEngine;
using Utilities;

namespace ClientMissions.Controllers{
    public class ActiveClient : MonoBehaviour{

        MissionData missionData;
        Dictionary<ClothingType, CombinedWearables> currentWearables = new Dictionary<ClothingType, CombinedWearables>();
        bool isActive;

        void Start(){
            DontDestroyOnLoad(this);
            if(isActive)
                return;
            EventBroker.Instance().SubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().SubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
            EventBroker.Instance().SubscribeMessage<SceneChangeMessage>(OnDressupSceneStart);
            EventBroker.Instance().SubscribeMessage<CurrentMissionClothesMessage>(CacheCurrentClothes);
            isActive = true;
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ActiveMissionMessage>(SelectMission);
            EventBroker.Instance().UnsubscribeMessage<ShowRewardMessage>(RemoveCurrentMission);
            EventBroker.Instance().UnsubscribeMessage<SceneChangeMessage>(OnDressupSceneStart);
            EventBroker.Instance().SubscribeMessage<CurrentMissionClothesMessage>(CacheCurrentClothes);
        }
        void CacheCurrentClothes(CurrentMissionClothesMessage currentMissionClothesMessage){
            currentWearables = currentMissionClothesMessage.CurrentWearables;
        }
        void OnDressupSceneStart(SceneChangeMessage sceneChangeMessage){
            if(missionData == null)
                return;
            EventBroker.Instance().SendMessage(new SendActiveMissionMessage(missionData, currentWearables));
        }
        void RemoveCurrentMission(ShowRewardMessage showRewardMessage){
            new LocalMissions().RemoveMission(missionData.SavableMissionData);
            missionData = null;
            currentWearables.Clear();
        }
        void SelectMission(ActiveMissionMessage activeMissionMessage){
            missionData = activeMissionMessage.MissionData;
            currentWearables.Clear();
        }
    }
}
