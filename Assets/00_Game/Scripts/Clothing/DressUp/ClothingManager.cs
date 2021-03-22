using ClientMissions.ClubMissions;
using Clothing.Upgrade;
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

        [SerializeField] GameObject clientShoesLeft;
        [SerializeField] GameObject clientShoesRight;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().SubscribeMessage<MessageUpCycleClothes>(UpCycleWearable);
            
            var currentShirt = FindObjectOfType<LastKnownClothes>().lastKnownShirt;
            var currentPants = FindObjectOfType<LastKnownClothes>().lastKnownPants;
           
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentShirt));
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentPants));

            clientShirtTorso.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;
            clientShirtArmLeft.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;
            clientShirtArmRight.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;

            clientPantsLeft.GetComponent<MeshRenderer>().material.mainTexture = currentPants.Texture;
            clientPantsRight.GetComponent<MeshRenderer>().material.mainTexture = currentPants.Texture;
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            switch (eventClothesChanged.Wearable.ClothingType.name) {
                case "Shirts":
                    clientShirtTorso.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.Wearable;

                    clientShirtArmLeft.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.Wearable;

                    clientShirtArmRight.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.Wearable;

                    break;
                case "Pants":
                    clientPantsLeft.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownPants = eventClothesChanged.Wearable;

                    clientPantsRight.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.Wearable.Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownPants = eventClothesChanged.Wearable;
                    break;
            }
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(eventClothesChanged.Wearable));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

        void UpCycleWearable(MessageUpCycleClothes messageUpCycleClothes) {
            Debug.Log("VAR");
        }
    }
}