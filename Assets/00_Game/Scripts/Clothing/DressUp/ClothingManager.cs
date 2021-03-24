using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        [SerializeField] List<GameObject> bodyParts;
        [SerializeField] List<GameObject> clothesCategories;
        [SerializeField] List<GameObject> clothingRarities;
        
        [SerializeField] LastKnownClothes lastKnownClothes;
        void Awake() {
            EventBroker.Instance().SubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().SubscribeMessage<RemoveAllClothes>(RemoveClothes);
            GetAllClothesCategories();
            GetAllBodyParts();
        }
        

        void RemoveClothes(RemoveAllClothes obj) {
            foreach (var rarity in clothingRarities) {
                foreach (Transform child in rarity.transform) {
                    child.gameObject.SetActive(false);
                }
            }
        }
        void GetAllClothesCategories() {
            foreach (var rarity in clothingRarities) {
                foreach (Transform clothingType in rarity.transform) {
                    clothesCategories.Add(clothingType.gameObject);
                }
            }
        }

        void GetAllBodyParts() {
            foreach (var rarity in clothingRarities) {
                foreach (Transform clothingType in rarity.transform) {
                    foreach (Transform bodyPart in clothingType) {
                        if (bodyPart != null)
                            bodyParts.Add(bodyPart.gameObject);
                    }
                }
            }
        }
        void Start() {
            lastKnownClothes = FindObjectOfType<LastKnownClothes>();
            
            //TODO! just for testing. remove later. client should only wear swimsuit from the start
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Shirts));
            //EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Pants));
            //EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Jackets));
        }
        

        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            bool clothesRemoved = CompareAndRemoveSelectedClothes(eventClothesChanged.CombinedWearables);

            if (clothesRemoved) {
                //update last known clothes (set to null)
                var field = typeof(LastKnownClothes).GetField(eventClothesChanged.CombinedWearables.clothingType.name);
                if (field == null) {
                    throw  new Exception("can't find the clothing type field in last known clothes!");
                }
                field.SetValue(lastKnownClothes, null);
                return;
            }
            
            EnableOrDisableCategory(eventClothesChanged.CombinedWearables);

            DressBodyParts(eventClothesChanged.CombinedWearables);
            
            //TODO: test that this works
            SetLastKnownItemOfType(eventClothesChanged.CombinedWearables);
        }

        void DressBodyParts(CombinedWearables combinedWearable) {
            //put on the different clothing parts on the different body parts
            foreach (var wearable in combinedWearable.wearable) {
                foreach (var bodyPart in bodyParts.Where(bodyPart => wearable.ClothingType.name == bodyPart.name)) {
                    bodyPart.GetComponent<MeshRenderer>().material.mainTexture = wearable.Texture;
                }
            }
            
            //Special case: When putting on a jacket -> Deactivate shirt sleeves
            if (combinedWearable.clothingType.name == "Jackets") {
                //deactivate the correct shirt sleeves (actually, de-activate all shirt sleeves, no-one will tell a difference)
                foreach (var bodyPart in bodyParts.Where(bodyPart => bodyPart.name == "ShirtLeftSleeve" || bodyPart.name == "ShirtRightSleeve")) {
                    bodyPart.gameObject.SetActive(false);
                }
            }
        }

        void EnableOrDisableCategory(CombinedWearables combinedWearable) {
            //enable/disable the clothes categories, based on the incoming new clothes
            //for example: we get basic pants
            //first disable all pants
            foreach (var category in clothesCategories) {
                if (category.name == combinedWearable.clothingType.name) {
                    category.SetActive(false);
                }
            }
            //enable basic pants
            var rarity = clothingRarities.Find(i => i.name == combinedWearable.rarity.name);
            var categoryToShow = rarity.transform.Find(combinedWearable.clothingType.name);
            categoryToShow.gameObject.SetActive(true);
        }

        bool CompareAndRemoveSelectedClothes(CombinedWearables combinedWearable) {
            if (combinedWearable == null) {
                throw new Exception("no combined wearables found");
            }
            
            if (GetLastKnownItemOfType(combinedWearable.clothingType.name) == combinedWearable) {
                RemoveClothesPiece(combinedWearable.rarity.name, combinedWearable.clothingType.name);
                
                //Special case: When removing a jacket -> activate shirt sleeves
                if (combinedWearable.clothingType.name == "Jackets") {
                    foreach (var bodyPart in bodyParts.Where(bodyPart => bodyPart.name == "ShirtLeftSleeve" || bodyPart.name == "ShirtRightSleeve")) {
                        bodyPart.gameObject.SetActive(true);
                    }
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
            var field = typeof(LastKnownClothes).GetField(clothingTypeName);
            if (field == null) {
                throw  new Exception("can't find the clothing type in last known clothes!");
            }
            return field.GetValue(lastKnownClothes) as CombinedWearables;
        }
        
        void SetLastKnownItemOfType(CombinedWearables combinedWearable) {
            var field = typeof(LastKnownClothes).GetField(combinedWearable.clothingType.name);
            if (field == null) {
                throw  new Exception("can't find the clothing type in last known clothes!");
            }
            field.SetValue(lastKnownClothes, combinedWearable);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }
    }
}