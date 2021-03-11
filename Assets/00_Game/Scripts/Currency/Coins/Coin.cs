using Cred._00_Game.Scripts.Currency.Coins;
using Cred.Scripts.SaveSystem;
using EventBrokerFolder;
using UnityEngine;

namespace Cred.Scripts
{
    public class Coin : MonoBehaviour, ISavable {
        SaveHandler saveHandler;
        public int _coin;
        public int Coins {
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
            Coins++;
            
        }
        public object ToBeSaved() {
            return Coins;
        }
        public void OnLoad(object value) {
            Coins = (int) value;
        }
    }
}
