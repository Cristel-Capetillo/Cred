using UnityEngine;
using Utilities;

namespace Clothing {
    public class ClothingManagerTest : MonoBehaviour {
        [SerializeField] Texture lastKnownShirt;
        [SerializeField] Texture lastKnownPants;
        
        [SerializeField] GameObject AlexTorso;
        [SerializeField] GameObject AlexPants;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);

            AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = lastKnownShirt;
            AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = lastKnownPants;
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            switch (eventClothesChanged.bodyPart) {
                case "Shirt":
                    AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    lastKnownShirt = eventClothesChanged.textureChanged;
                    break;
                case "Pants":
                    AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.textureChanged;
                    lastKnownPants = eventClothesChanged.textureChanged;
                    break;
            }
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }
    }
}