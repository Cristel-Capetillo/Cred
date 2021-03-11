using System;
using System.Collections.Generic;
using Cred.Scripts.Clothing;
using UnityEngine;
using EventBrokerFolder;

namespace Cred._00_Game.Scripts.Clothing
{
    public class InventoryDataHandler : MonoBehaviour
    {
        [SerializeField]List<Wearable> wearableList = new List<Wearable>();

        void Start(){
            EventBroker.Instance().SubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }
        void OnDestroy(){
           EventBroker.Instance().UnsubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }
        void OnLoadWearablesAssets(WearableListMessage wearableListMessage){
            print("Test");
            //if dictionary.contains[test] only add the wearable
            wearableList = wearableListMessage.Wearables;
            //Sort them into preferably a dictionary 
            //Amount of a specific item should come from the database server.
        }
        void OnLoadWearableData(){
            throw new Exception("Not implemented yet!");
        }
    }

    public class WearableListMessage{
        public List<Wearable> Wearables;

        public WearableListMessage(List<Wearable> wearables){
            Wearables = wearables;
        }
    }
}
