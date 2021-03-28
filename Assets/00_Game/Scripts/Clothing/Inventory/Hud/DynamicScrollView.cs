using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Clothing.Inventory {
    public class DynamicScrollView : MonoBehaviour {
        int multiplier;

        [SerializeField] Transform[] rarities;

        void Start() {
            CalculateHeight();
        }

        void CalculateHeight() {
            multiplier = 0;
            multiplier = rarities.Sum(x => x.transform.childCount);

            //multiplier = transform.childCount / 2;

            var rect = GetComponent<RectTransform>().sizeDelta;
            rect.y = multiplier * 121.1f;
            rect.y -= 121.1f * 3;
            GetComponent<RectTransform>().sizeDelta = rect;
        }
    }
}