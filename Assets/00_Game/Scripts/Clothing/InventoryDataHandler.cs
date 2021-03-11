using System;
using System.Collections.Generic;
using Cred.Scripts.Clothing;
using UnityEngine;
using EventBrokerFolder;

namespace Cred._00_Game.Scripts.Clothing
{
    public class InventoryDataHandler : MonoBehaviour
    {
        public Dictionary<ClothingType, List<Wearable>> WearableDictionary = new Dictionary<ClothingType, List<Wearable>>();

        void Start(){
            EventBroker.Instance().SubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }
        void OnDestroy(){
           EventBroker.Instance().UnsubscribeMessage<WearableListMessage>(OnLoadWearablesAssets);
        }
        void OnLoadWearablesAssets(WearableListMessage wearableListMessage){
            print("Test");
            var temp = wearableListMessage.Wearables;
            if (!WearableDictionary.ContainsKey(temp[0].ClothingType)) {
                WearableDictionary.Add(temp[0].ClothingType, temp);
            }
            else {
                WearableDictionary[temp[0].ClothingType].AddRange(temp);
            }
            Debug.Log(WearableDictionary.Count + " WearableDictonaryCount");
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
