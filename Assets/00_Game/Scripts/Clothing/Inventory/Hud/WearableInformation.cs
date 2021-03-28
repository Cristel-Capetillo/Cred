﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Inventory {
    public class WearableInformation : MonoBehaviour {
        [SerializeField] Text stylePointsText;
        [SerializeField] Text amountText;
        [SerializeField] Image[] colorDataImages;

        CombinedWearables wearables;

        void Start() {
            //EventBroker.Instance().SubscribeMessage<EventUpdateWearableInfo>(UpdateInformation);
        }

        void OnEnable() {
            wearables = GetComponent<CombinedWearables>();

            UpdateInformation();
        }

        void OnDestroy() {
            //EventBroker.Instance().UnsubscribeMessage<EventUpdateWearableInfo>(UpdateInformation);
        }

        void UpdateInformation(EventUpdateWearableInfo newInfo) {
            stylePointsText.text = wearables.stylePoints.ToString();
            amountText.text = "x" + wearables.Amount;
        }

        void UpdateInformation() {
            stylePointsText.text = wearables.stylePoints.ToString();
            amountText.text = "x" + wearables.Amount;

            foreach (var image in colorDataImages) {
                image.enabled = false;
            }

            for (var i = 0; i < wearables.wearable.Count; i++) {
                var wearable = wearables.wearable[i];
                switch (i) {
                    case 0:
                        colorDataImages[i].enabled = true;
                        colorDataImages[i].color = wearable.colorData.color;
                        break;
                    case 1:
                        colorDataImages[i].enabled = true;
                        colorDataImages[i].color = wearable.colorData.color;
                        break;

                    case 2:
                        colorDataImages[i].enabled = true;
                        colorDataImages[i].color = wearable.colorData.color;
                        break;
                }
            }
        }
    }
}