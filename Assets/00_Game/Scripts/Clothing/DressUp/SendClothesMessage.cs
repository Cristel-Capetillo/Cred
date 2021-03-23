using UnityEngine;
using Utilities;

namespace Clothing {
    public class SendClothesMessage : MonoBehaviour {
        [SerializeField] CombinedWearables defaultShirt;
        [SerializeField] CombinedWearables defaultPants;
        

        public void ChangeDefaultClothes() {
            EventBroker.Instance().SendMessage(new EventClothesChanged(defaultShirt));
            EventBroker.Instance().SendMessage(new EventClothesChanged(defaultPants));
        }
    }
}