using Cred.Scripts.SaveSystem;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Scripts
{
    public class Coin : MonoBehaviour, ISavable {
        SaveHandler saveHandler;
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
            saveHandler = new SaveHandler(this.name);
            // saveHandler.Load(this);
        }

        public object ToBeSaved() {
            return Coins;
        }

        public void OnLoad(object value) {
            Coins = (int) value;
        }
    }

    public class EventCoinChanged {
        public readonly int Coins;

        public EventCoinChanged(int coins) {
            Coins = coins;
        }
    }
}
