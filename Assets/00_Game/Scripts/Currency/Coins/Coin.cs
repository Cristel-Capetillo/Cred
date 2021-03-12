using Cred._00_Game.Scripts.Currency.Coins;
using Cred.Scripts.SaveSystem;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Coins {
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
            saveHandler = new SaveHandler(this.name);
            //saveHandler.Load(this);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                Coins++;
                print(Coins);
            }
            
            if (Input.GetKeyDown(KeyCode.S)) {
                saveHandler.Save(this);
                print($"Saved {Coins}");
            }
            
            if (Input.GetKeyDown(KeyCode.L)) {
                saveHandler.Load(this);
            }
        }

        public long ToBeSaved() {
            return Coins;
        }

        public void OnLoad(long value) {
            Coins = value;
            Debug.Log($"Coins: {Coins}");
        }
    }
}