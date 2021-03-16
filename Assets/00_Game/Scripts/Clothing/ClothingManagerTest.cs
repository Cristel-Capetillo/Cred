using UnityEngine;
using Utilities;

namespace Clothing {
    public class ClothingManagerTest : MonoBehaviour {
        
        //TODO: Add a reset button to go back to default
        

        [SerializeField] GameObject AlexTorso;
        [SerializeField] GameObject AlexPants;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            print("eventclothesChanged called");
            switch (eventClothesChanged.bodyPart) {
                case "Shirt":
                    AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    break;
                case "Pants":
                    AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    break;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }
    }
}