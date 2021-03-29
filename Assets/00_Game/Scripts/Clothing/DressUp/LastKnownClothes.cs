using System;
using Core;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp
{
    public class LastKnownClothes : MonoBehaviour {
        public CombinedWearables Shirts;
        public CombinedWearables Jackets;
        public CombinedWearables Pants;
        public CombinedWearables Skirts;
        public CombinedWearables Shoes;
        public CombinedWearables Accessories;

        public int hasReloadedScene;

        void Awake() {
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSceneSwap>(UpdateBool);
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventSceneSwap>(UpdateBool);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }

        void RemoveClothes(RemoveAllClothes allClothes) {
            //reset all last known clothes
            Shirts = null;
            Pants = null;
            Skirts = null;
            Jackets = null;
            Shoes = null;
            Accessories = null;
        }
        void UpdateBool(EventSceneSwap tmp) {
            hasReloadedScene = 6;

        }
    }
}
