using Cred._00_Game.Scripts.Currency.Coins;
using Cred.Scripts.SaveSystem;
using EventBrokerFolder;
using UnityEngine;

namespace _00_Game.Scripts.Currency.Coins {
    public class Coin : MonoBehaviour, ISavable<long> {
        SaveHandler saveHandler;
        public long _coin;
        public long Coins {
            get => _coin;
            set {
                _coin = value;
                saveHandler.Save(this);
                EventBroker.Instance().SendMessage(new EventCoinChanged(_coin));
            }
        }

        void Start() {
            saveHandler = new SaveHandler(name);
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