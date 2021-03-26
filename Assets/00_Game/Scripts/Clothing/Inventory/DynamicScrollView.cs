using System;
using UnityEngine;

namespace Clothing.Inventory {
    public class DynamicScrollView : MonoBehaviour {
        //value bigger than 6 and height = 120

        public CombinedWearables combinedWearablesTemplate;
        int multiplier;

        float height;

        void Start() {
            height = combinedWearablesTemplate.GetComponent<RectTransform>().sizeDelta.y;
            CalculateHeight();
        }

        void CalculateHeight() {
            multiplier = 0;
            for (var i = 6; i < transform.childCount; i += 2) {
                multiplier++;
            }

            var rect = GetComponent<RectTransform>().sizeDelta;
            rect.y = rect.y + (multiplier * height);
            GetComponent<RectTransform>().sizeDelta = rect;

        }
    }
}