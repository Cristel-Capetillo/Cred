using _00_Game.Scripts.Clothing;
using Cred.Scripts.Clothing;
using EventBrokerFolder;
using UnityEngine;

namespace Cred
{
    public class SendClothesMessage : MonoBehaviour
    {
        [SerializeField]Wearable wearable;

        public void ChangeClothes() {
            EventBroker.Instance().SendMessage(new EventClothesChanged(wearable));
        }
    }
}
