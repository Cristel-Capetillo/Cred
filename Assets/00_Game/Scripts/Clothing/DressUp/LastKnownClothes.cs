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

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventSceneSwap>(UpdateBool);
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventSceneSwap>(UpdateBool);
        }

        void UpdateBool(EventSceneSwap tmp) {
            hasReloadedScene = 6;

        }
    }
}
