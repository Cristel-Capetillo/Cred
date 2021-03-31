using System;
using System.Collections.Generic;
using Clothing.Inventory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.PlayerLoop;
using Utilities;

namespace Clothing {
    public class CombinedWearables : MonoBehaviour {
        public List<Wearable> wearable;
        public ClothingType clothingType;
        public Rarity rarity;
        public int stylePoints;

        public string rewardMessage;
        public bool showText;

        public bool isPredefined = true;

        [SerializeField] CanvasGroup canvasGroup;
        int _amount;
        public int Amount {
            get => _amount;
            set => _amount = Mathf.Clamp(value, 0, int.MaxValue);
        }

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventDestroyCombinedWearable>(DestroyMe);
            EventBroker.Instance().SubscribeMessage<EventUpdateAmount>(UpdateZeAmount);
        }
        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventDestroyCombinedWearable>(DestroyMe);
            EventBroker.Instance().UnsubscribeMessage<EventUpdateAmount>(UpdateZeAmount);
        }

        void UpdateZeAmount(EventUpdateAmount amount) {
            if (amount.id == ToString()) {
                Amount = amount.amount;
            }
        }

        public override string ToString() {
            var uID = "";
            foreach (var wearable1 in wearable) {
                uID += wearable1.ClothingType.name;
                uID += wearable1.colorData.name;
            }

            return uID + rarity.name + clothingType.name + stylePoints;
        }

        public void ShouldBeInteractable() {
            // if (Amount > 0) {
            //     canvasGroup.alpha = 1;
            //     canvasGroup.interactable = true;
            //     canvasGroup.blocksRaycasts = true;
            // }
            // else {
            //     canvasGroup.alpha = .5f;
            //     canvasGroup.interactable = false;
            //     canvasGroup.blocksRaycasts = false;
            // }
        }

        void DestroyMe(EventDestroyCombinedWearable destroy) {
            if (destroy.id == ToString())
                Destroy(gameObject);
        }

        public void AddStylePoint() {
            if (stylePoints < rarity.MaxValue) {
                stylePoints++;
            }
        }

        public void SetStylePoints(int sp) {
            stylePoints = sp - rarity.Value;
        }
    }

    public class EventDestroyCombinedWearable {
        public readonly string id;

        public EventDestroyCombinedWearable(string id) {
            this.id = id;
        }
    }
}