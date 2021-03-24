using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        [SerializeField] List<GameObject> bodyParts;
        [SerializeField] List<GameObject> clothingRarities;
        
        LastKnownClothes lastKnownClothes;
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(RemoveClothes);
            
            GetAllBodyParts();
        }
        void GetAllBodyParts() {
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
            
            bool clothesRemoved = CompareAndRemoveSelectedClothes(eventClothesChanged.CombinedWearables);

            if (clothesRemoved) {
                //update last known clothes (null or empty)
                var clothingType = eventClothesChanged.CombinedWearables.clothingType.name;
                FieldInfo clothesProperty = typeof(LastKnownClothes).GetField(clothingType);
                if (clothesProperty == null) {
                    throw  new Exception("can't find the clothing type in last known clothes!");
                }
                CombinedWearables lastKnownClothingItem = clothesProperty.GetValue(lastKnownClothes) as CombinedWearables;
                
                //return;
            }
            
            //TODO: disable/enable objects based on the rarity of the new clothes
            EnableOrDisableCategory(eventClothesChanged.CombinedWearables);
            
  
            //TODO: put on the actual new clothes
            DressBodyParts(eventClothesChanged.CombinedWearables);
            
            
            //TODO: Update last known clothing item
            // FindObjectOfType<LastKnownClothes>().lastKnownShirt = eventClothesChanged.CombinedWearables.clothingtype;
            //use Type.GetProperty/SetValue(Object, Object)
            
        }

        void DressBodyParts(CombinedWearables combinedWearable) {
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
            if (combinedWearable.clothingType.name == "Jackets") {
                //deactivate the correct shirt sleeves
            }
        }

        void EnableOrDisableCategory(CombinedWearables combinedWearable) {
            //enable/disable the objects, based on the incoming new clothes
            //for example: we get basic pants
            //disable all pants
            //enable basic pants
        }

        bool CompareAndRemoveSelectedClothes(CombinedWearables combinedWearable) {
            if (combinedWearable == null) {
                throw new Exception("no combined wearables found");
            }
            
            if (GetLastKnownItemOfType(combinedWearable.clothingType.name) == combinedWearable) {
                RemoveClothesPiece(combinedWearable.rarity.name, combinedWearable.clothingType.name);
                
                //TODO: Special case: When removing a jacket -> activate shirt sleeves
                //TODO: but first check if a shirt is currently equipped
                if (combinedWearable.clothingType.name == "Jackets") {
                    var shirtRarity = lastKnownClothes.Shirts.rarity.name;
                    //TODO : complete this
                }

                return true;
            }
            return false;
        }

        void RemoveClothesPiece(string rarityName, string clothingTypeName) {
            var rarity = clothingRarities.Find(i => i.name == rarityName);
            var clothTransform = rarity.transform.Find(clothingTypeName);
            clothTransform.gameObject.SetActive(false);
        }

        CombinedWearables GetLastKnownItemOfType(string clothingTypeName) {
            var clothesProperty = typeof(LastKnownClothes).GetField(clothingTypeName);
            if (clothesProperty == null) {
                throw  new Exception("can't find the clothing type in last known clothes!");
            }
            return clothesProperty.GetValue(lastKnownClothes) as CombinedWearables;
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }
    }
}