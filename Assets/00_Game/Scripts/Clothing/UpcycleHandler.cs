using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Clothing
{
    public class UpcycleHandler : MonoBehaviour
    {

        void Start()
        {
            EventBroker.Instance().SubscribeMessage<MessageUpCycleClothes>(OnLoadWearablesAssets);
        }

        void OnDestroy()
        {
            EventBroker.Instance().UnsubscribeMessage<MessageUpCycleClothes>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(MessageUpCycleClothes messageUpCycleClothes)
        {
        
            Wearable item1 = messageUpCycleClothes.Wearable1;
            Wearable item2 = messageUpCycleClothes.Wearable2;
      
            
        }

        void OnLoadWearableData()
        {
            throw new Exception("Not implemented yet!");
        }

        //public class UpCyclingMessage
        //{
        //    public GameObject wearable1;
        //    public GameObject wearable2;
        //    public UpCyclingMessage(GameObject firstWearable, GameObject secondWearable)
        //    {
        //        firstWearable = wearable1;
        //        secondWearable = wearable2;
        //    }
        //}
    }
}


