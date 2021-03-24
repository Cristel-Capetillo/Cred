using System;
using System.Collections.Generic;
using ClientMissions.ClubMissions;
using ClientMissions.MissionMessages;
using Clothing.Upgrade;
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
           
            // EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentShirt));
            // EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentPants));
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(currentJacket));
            
            //test code:dress up guy in default jacket
            EventBroker.Instance().SendMessage(new EventClothesChanged(currentJacket));
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            //TODO: change the switch to a nested foreach() too loop through all received clothes and match with the corresponding body parts
            //TODO: make sure the different limbs have the same name as the wearables body parts
            
            /*foreach (var wearable in eventClothesChanged.CombinedWearables.wearable) {
                foreach (var bodyPart in bodyParts) {
                    if (wearable.ClothingType.name == bodyPart.name) {
                        bodyPart.GetComponent<MeshRenderer>().material.mainTexture = wearable.Texture;
                    }
                    
                }
                
            }*/
            
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
            EventBroker.Instance().SendMessage(new EventWearableStylePoints(eventClothesChanged.CombinedWearables));
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
        }

       
    }
}