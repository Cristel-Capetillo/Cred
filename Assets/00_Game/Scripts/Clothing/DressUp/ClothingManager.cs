using ClientMissions.ClubMissions;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        [SerializeField] GameObject AlexTorso;
        [SerializeField] GameObject AlexPants;
        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            
            var currentShirt = FindObjectOfType<LastKnownClothes>().lastKnownShirt;
            var currentPants = FindObjectOfType<LastKnownClothes>().lastKnownPants;
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentShirt));
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentPants));
            
            AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = currentShirt.Texture;
            AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = currentPants.Texture;
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            switch (eventClothesChanged.Wearable.ClothingType.name) {
                case "Shirts":
                    AlexTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.Wearable;
                    break;
                case "Pants":
                    AlexPants.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownPants = eventClothesChanged.Wearable;
                    break;
            }
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(eventClothesChanged.Wearable));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }
    }
}