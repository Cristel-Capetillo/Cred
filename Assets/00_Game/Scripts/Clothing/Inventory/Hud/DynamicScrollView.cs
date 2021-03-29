using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Inventory {
    public class DynamicScrollView : MonoBehaviour {
        [SerializeField] RectTransform[] childRects;

        RectTransform thisRect;

        void Start() {
            thisRect = GetComponent<RectTransform>();
            EventBroker.Instance().SubscribeMessage<EventFinishedLoadingPlayerInventory>(CalculateHeight);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventFinishedLoadingPlayerInventory>(CalculateHeight);
        }

        void CalculateHeight(EventFinishedLoadingPlayerInventory contentHeight) {
            var rect = thisRect.sizeDelta;
            rect.y = childRects.Sum(x => x.sizeDelta.y);


            thisRect.sizeDelta = rect;
        }
    }
}