using UnityEngine;
using Utilities;

namespace Clothing {
    public class SendClothesMessage : MonoBehaviour {
        [SerializeField] Wearable defaultShirt;
        [SerializeField] Wearable defaultPants;
        

        public void ChangeDefaultClothes() {
            EventBroker.Instance().SendMessage(new EventClothesChanged(defaultShirt));
            EventBroker.Instance().SendMessage(new EventClothesChanged(defaultPants));
        }
    }
}