using ClientMissions.ClubMissions;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {

        [SerializeField] GameObject clientShirtTorso;
        [SerializeField] GameObject clientJacketTorso;

        [SerializeField] GameObject clientShirtArmLeft;
        [SerializeField] GameObject clientShirtArmRight;
        [SerializeField] GameObject clientJacketArmLeft;
        [SerializeField] GameObject clientJacketArmRight;

        [SerializeField] GameObject clientPantsRight;
        [SerializeField] GameObject clientPantsLeft;
        [SerializeField] GameObject clientSkirtLegs;


        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            
            var currentShirt = FindObjectOfType<LastKnownClothes>().lastKnownShirt;
            var currentPants = FindObjectOfType<LastKnownClothes>().lastKnownPants;
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentShirt));
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentPants));
            
            clientShirtTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = currentShirt.Texture;
            clientPantsLeft.GetComponent<SkinnedMeshRenderer>().material.mainTexture = currentPants.Texture;
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            switch (eventClothesChanged.Wearable.ClothingType.name) {
                case "Shirts":
                    clientShirtTorso.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.Wearable;
                    break;
                case "Pants":
                    clientPantsLeft.GetComponent<SkinnedMeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
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