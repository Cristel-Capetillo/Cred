using System;
using MysteryBox;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace HUD.MysteryBox {
    public class MysteryBoxInventory : MonoBehaviour, ISavable<long> {
        
        SaveHandler saveHandler;
        
        public int Owned {
            get => _owned;
            private set {
                _owned = value;
                saveHandler.Save(this);
            }
        }
        int _owned;

        void Start() {
            saveHandler = new SaveHandler("MysteryBoxInventory");
            saveHandler.Load(this);
            EventBroker.Instance().SubscribeMessage<EventMysteryBoxBought>(OnMysteryBoxPurchase);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventMysteryBoxBought>(OnMysteryBoxPurchase);
        }

        void OnMysteryBoxPurchase(EventMysteryBoxBought eventMysteryBoxBought) {
            Owned++;
        }

        public long ToBeSaved() {
            return Owned;
        }

        public void OnLoad(long value) {
            Owned = (int) value;
        }
    }
}