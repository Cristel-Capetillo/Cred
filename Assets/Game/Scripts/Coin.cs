using System;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Scripts
{
    public class Coin : MonoBehaviour {
        public int _coin;

        public int Coins {
            get {
                return _coin;
            }
            set {
                _coin = value;
                EventBroker.Instance().SendMessage(new EventCoinChanged(_coin));
            }
        }
        void Start() {
            Coins++;
        }
    }

    public class EventCoinChanged {
        public readonly int Coins;

        public EventCoinChanged(int coins) {
            Coins = coins;
        }
    }
}
