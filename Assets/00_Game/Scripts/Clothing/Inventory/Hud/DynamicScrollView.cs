using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing.Inventory {
    public class DynamicScrollView : MonoBehaviour {
        [SerializeField] RectTransform[] childRects;

        RectTransform thisRect;

        void Start() {
            thisRect = GetComponent<RectTransform>();
            EventBroker.Instance().SubscribeMessage<EventUpdateWearableHud>(CalculateHeight);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateWearableHud>(CalculateHeight);
        }

        void CalculateHeight(EventUpdateWearableHud contentHeight) {
            var rect = Vector2.zero;

            //rect.y = childRects.Sum(x => x.sizeDelta.y + 10);

            foreach (var childRect in this.childRects) {
                rect.y += childRect.sizeDelta.y + 10;
            }

            thisRect.sizeDelta = rect;
        }
    }
}