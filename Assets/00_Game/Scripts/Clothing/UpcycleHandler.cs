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
            EventBroker.Instance().SubscribeMessage<UpCyclingMessage>(OnLoadWearablesAssets);
        }

        void OnDestroy()
        {
            EventBroker.Instance().UnsubscribeMessage<UpCyclingMessage>(OnLoadWearablesAssets);
        }

        void OnLoadWearablesAssets(UpCyclingMessage upCyclingMessage)
        {
            GameObject item1 = upCyclingMessage.wearable1;
            GameObject item2 = upCyclingMessage.wearable2;
      
            
        }

        void OnLoadWearableData()
        {
            throw new Exception("Not implemented yet!");
        }

        public class UpCyclingMessage
        {
            public GameObject wearable1;
            public GameObject wearable2;
            public UpCyclingMessage(GameObject firstWearable, GameObject secondWearable)
            {
                firstWearable = wearable1;
                secondWearable = wearable2;
            }
        }
    }
}


