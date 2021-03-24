using System.Collections.Generic;
using System.Linq;
using ClientMissions.ClubMissions;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        //TODO: put all body parts in a list. make sure the bodyparts have the same names as
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

        [SerializeField] List<GameObject> bodyParts;

        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

        void Start() {
            
            var currentShirt = FindObjectOfType<LastKnownClothes>().lastKnownShirt;
            var currentPants = FindObjectOfType<LastKnownClothes>().lastKnownPants;
            var currentJacket = FindObjectOfType<LastKnownClothes>().lastKnownJacket;
            
  
            
            //test code:dress up guy in starting jacket
            EventBroker.Instance().SendMessage(new EventClothesChanged(currentJacket));
            //TODO: add more starting clothes
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            //TODO: When clicking on clothes already equipped, they should be removed.
            //possibly calculate item ID and compare to the one equipped
            
            //TODO: When removing a jacket -> activate shirt sleeves
            if (eventClothesChanged.CombinedWearables.clothingType.SingularName == "Jackets") {
                //activate the correct shirt sleeves
            }
            
            //TODO: disable/enable objects based on the rarity of the new clothes
            
            
            //TODO: change the switch to a nested foreach() too loop through all received clothes and match with the corresponding body parts
            //TODO: make sure the different limbs have the same name as the wearables body parts
            
            /*foreach (var wearable in eventClothesChanged.CombinedWearables.wearable) {
                foreach (var bodyPart in bodyParts) {
                    if (wearable.ClothingType.name == bodyPart.name) {
                        bodyPart.GetComponent<MeshRenderer>().material.mainTexture = wearable.Texture;
                    }
                    
                }
                
            }*/
            

            
            //TODO: When putting on a jacket -> Deactivate shirt sleeves
            if (eventClothesChanged.CombinedWearables.clothingType.SingularName == "Jackets") {
                //deactivate the correct shirt sleeves
            }
            
            //TODO: Update last known clothing item
            // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables.clothingtype;
            //use Type.GetProperty/SetValue(Object, Object)
            
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
                case "Jackets":
                    Debug.Log("put on a brand new jacket!");
                    clientJacketTorso.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.wearable[0].Texture;
                    clientJacketArmLeft.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.wearable[1].Texture;
                    clientJacketArmRight.GetComponent<MeshRenderer>().material.mainTexture = eventClothesChanged.CombinedWearables.wearable[2].Texture;
                    FindObjectOfType<LastKnownClothes>().lastKnownJacket = eventClothesChanged.CombinedWearables;
                    break;
            }
            
            //last, update the stylepoints
            //TODO: how does it work when taking off clothes?
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(eventClothesChanged.CombinedWearables));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

       
    }
}