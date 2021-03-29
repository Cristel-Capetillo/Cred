using System;
using ClientMissions.Data;
using ClientMissions.Messages;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace ClientMissions.Controllers{
    public class PlayerFollowers : MonoBehaviour, IFollowers, ISavable<string>{
        [SerializeField] int maxFollowers = 1000;
        [SerializeField] int followers;
        SaveHandler saveHandler;
        

        void Start(){
            saveHandler = new SaveHandler("Followers");
            saveHandler.Load(this);
            EventBroker.Instance().SubscribeMessage<UpdateFollowersMessage>(UpdateFollowers);
        }
        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<UpdateFollowersMessage>(UpdateFollowers);
        }
        void UpdateFollowers(UpdateFollowersMessage followersMessage){
            followers += followersMessage.amountToUpdate;
            saveHandler.Save(this);
            EventBroker.Instance().SendMessage(new UpdateUIFollowersMessage(followers));
        }

        public int Followers => followers;
        public int MaxFollowers => maxFollowers;
        
        public string ToBeSaved(){
            print("Save followers");
            return followers.ToString();
        }

        public void OnLoad(string value){
            print("Load followers");
            followers = Convert.ToInt32(value);
            EventBroker.Instance().SendMessage(new UpdateUIFollowersMessage(followers));
        }
    }
}