using System;
using Core;
using HUD;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace Currency.Coins {
    public class Coin : MonoBehaviour, ISavable<long> {
        SaveHandler saveHandler;
        public long _coin;
        public long Coins {
            get => _coin;
            set {
                _coin = value;
                saveHandler.Save(this);
                EventBroker.Instance().SendMessage(new EventUpdateUICoins(_coin));
            }
        }

        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventUpdateCoins>(UpdateCoins);
            EventBroker.Instance().SubscribeMessage<EventSceneSwap>(UpdateOnNewScene);
        }

        void Start() {
            saveHandler = new SaveHandler(name);
            saveHandler.Load(this);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateCoins>(UpdateCoins);
            EventBroker.Instance().UnsubscribeMessage<EventSceneSwap>(UpdateOnNewScene);
        }

        void UpdateCoins(EventUpdateCoins updateCoins) {
            Coins += updateCoins.amountToUpdate;
        }

        void UpdateOnNewScene(EventSceneSwap nextScene) {
            if (!nextScene.newScene) return;
            saveHandler.Load(this);
        }

        public long ToBeSaved() {
            return Coins;
        }

        public void OnLoad(long value) {
            Coins = value;
        }
    }
}