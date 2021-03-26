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
            multiplier = transform.childCount / 2;
            
            var rect = GetComponent<RectTransform>().sizeDelta;
            rect.y += multiplier * 121.1f;
            GetComponent<RectTransform>().sizeDelta = rect;

        }
    }
}