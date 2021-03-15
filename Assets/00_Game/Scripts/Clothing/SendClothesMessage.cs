using UnityEngine;
using Utilities;

namespace Clothing
{
    public class SendClothesMessage : MonoBehaviour
    {
        [SerializeField]Wearable wearable;

        public void ChangeClothes() {
            EventBroker.Instance().SendMessage(new EventClothesChanged(wearable));
        }
    }
}
