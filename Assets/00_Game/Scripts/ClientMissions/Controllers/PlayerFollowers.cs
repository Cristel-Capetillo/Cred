using System;
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
        }

        public int Followers => followers;
        public int MaxFollowers => maxFollowers;
        
        public string ToBeSaved(){
            return followers.ToString();
        }

        public void OnLoad(string value){
            followers = Convert.ToInt32(value);
        }
    }
}