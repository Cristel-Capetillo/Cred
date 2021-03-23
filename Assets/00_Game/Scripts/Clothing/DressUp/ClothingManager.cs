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
            
            var currentShirt = FindObjectOfType<LastKnownClothes>().lastKnownShirt;
            var currentPants = FindObjectOfType<LastKnownClothes>().lastKnownPants;
           
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentShirt));
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentPants));

            //
            // clientShirtTorso.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;
            // clientShirtArmLeft.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;
            // clientShirtArmRight.GetComponent<MeshRenderer>().material.mainTexture = currentShirt.Texture;
            //
            // clientPantsLeft.GetComponent<MeshRenderer>().material.mainTexture = currentPants.Texture;
            // clientPantsRight.GetComponent<MeshRenderer>().material.mainTexture = currentPants.Texture;
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            switch (eventClothesChanged.CombinedWearables.clothingType.name) {
                case "Shirts":
                    // clientShirtTorso.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.Texture;
                    // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables;
                    //
                    // clientShirtArmLeft.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.Texture;
                    // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables;
                    //
                    // clientShirtArmRight.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.Texture;
                    // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables;

                    break;
                case "Pants":
                    // clientPantsLeft.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.Texture;
                    // FindObjectOfType<LastKnownClothes>().lastKnownPants = eventClothesChanged.CombinedWearables;
                    //
                    // clientPantsRight.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.Texture;
                    // FindObjectOfType<LastKnownClothes>().lastKnownPants = eventClothesChanged.CombinedWearables;
                    break;
            }
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(eventClothesChanged.CombinedWearables));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

       
    }
}