using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class SendClothesMessage : MonoBehaviour {
    public void ChangeDefaultClothes() {
            EventBroker.Instance().SendMessage(new RemoveAllClothes());
    }
    }
}