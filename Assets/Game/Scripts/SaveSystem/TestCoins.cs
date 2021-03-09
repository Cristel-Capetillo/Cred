using System;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Scripts.SaveSystem {
    public class TestCoins : MonoBehaviour, ISavable {
        
        int _coins;
        SaveHandler saveHandler;

        public int Coins {
            get => _coins;
            set {
                _coins = value;
                EventBroker.Instance().SendMessage(new EventCoinsChanged(_coins));
            }
        }

        void Start() {
            saveHandler = new SaveHandler(this.name);
            saveHandler.Load(this);
        }

        void OnDestroy() {
           saveHandler.Save(this); 
        }

        //methods
        public object ToBeSaved() {
            return Coins;
        }
        public void OnLoad(object value) {
            Coins = (int)value;
        }
    }

    public class tmp : MonoBehaviour{ //Coin GUI?
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventCoinsChanged>(UpdateCoinsTextOnScreen);
        }

        void UpdateCoinsTextOnScreen(EventCoinsChanged eventCoinsChanged) {
            Debug.Log($"{eventCoinsChanged.coins}");
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventCoinsChanged>(UpdateCoinsTextOnScreen);
        }
    }

    public class EventCoinsChanged {
        
        public readonly int coins;

        public EventCoinsChanged(int coins) {
            this.coins = coins;
        }
    }
}