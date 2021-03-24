using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        //TODO: put all body parts in a list. make sure the bodyparts have the same names as in code
        [SerializeField] List<GameObject> bodyParts;
        [SerializeField] List<GameObject> clothingRarities;
        
        LastKnownClothes lastKnownClothes;
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(RemoveClothes);
            
            foreach (var rarity in clothingRarities) {
                foreach (Transform clothingType in rarity.transform) {
                    foreach (Transform bodyPart in clothingType) {
                        if(bodyPart!=null)
                            bodyParts.Add(bodyPart.gameObject);
                    }
                }
            }
           
        }

        void RemoveClothes(RemoveAllClothes obj) {
            foreach (var rarity in clothingRarities) {
                foreach (Transform child in rarity.transform) {
                    child.gameObject.SetActive(false);
                }
            }
        }

        void Start() {
            lastKnownClothes = FindObjectOfType<LastKnownClothes>();
            
            //dress up client in starting clothes
            //TODO! just for testing. remove later. client should only wear swimsuit from the start
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Shirts));
            //EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Pants));
            //EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Jackets));
        }

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            
            //TODO: When clicking on clothes already equipped, they should be removed.
            bool clothesRemoved = CompareAndRemoveSelectedClothes(eventClothesChanged);

            if (clothesRemoved) {
                //update last known clothes (null or empty)
                
                //return;
            }
            
            //TODO: disable/enable objects based on the rarity of the new clothes
            EnableOrDisableCategory(eventClothesChanged);
            
  
            //TODO: put on the actual new clothes
            DressBodyParts(eventClothesChanged);
            
            
            //TODO: Update last known clothing item
            // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables.clothingtype;
            //use Type.GetProperty/SetValue(Object, Object)
            
        }

        void DressBodyParts(EventClothesChanged eventClothesChanged) {
            //put on the different clothing parts on the different body parts
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
            if (eventClothesChanged.CombinedWearables.clothingType.name == "Jackets") {
                //deactivate the correct shirt sleeves
            }
        }

        void EnableOrDisableCategory(EventClothesChanged eventClothesChanged) {
            //enable/disable the objects, based on the incoming new clothes
            //for example: we get basic pants
            //disable all pants
            //enable basic pants
        }

        bool CompareAndRemoveSelectedClothes(EventClothesChanged eventClothesChanged) {
            //TODO: When clicking on clothes already equipped, they should be removed.
            //possibly calculate item ID and compare to the one equipped
            //or just compare the objects

            if (eventClothesChanged.CombinedWearables == null) {
                throw new Exception("no combined wearables found");
            }
            var clothingType = eventClothesChanged.CombinedWearables.clothingType.name;
            FieldInfo clothesProperty = typeof(LastKnownClothes).GetField(clothingType);
            if (clothesProperty == null) {
                throw  new Exception("can't find the clothing type in last known clothes!");
            }
            CombinedWearables lastKnownClothingItem = clothesProperty.GetValue(lastKnownClothes) as CombinedWearables;

            if (lastKnownClothingItem == eventClothesChanged.CombinedWearables) {
                //remove this piece of clothes
                var rarity = clothingRarities.Find(i => i.name == eventClothesChanged.CombinedWearables.rarity.name);
                var clothTransform = rarity.transform.Find(eventClothesChanged.CombinedWearables.clothingType.name);
                clothTransform.gameObject.SetActive(false);
                
                //TODO: Special case: When removing a jacket -> activate shirt sleeves
                //TODO: but first check if a shirt is currently equipped
                if (eventClothesChanged.CombinedWearables.clothingType.name == "Jackets") {
                    var shirtRarity = lastKnownClothes.Shirts.rarity.name;
                    //TODO : complete this
                }

                return true;
            }
            return false;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }
    }
}