using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Clothing.DressUp {
    public class ClothingManager : MonoBehaviour {
        [SerializeField] List<GameObject> clothingRarities;
        [SerializeField] List<GameObject> clothesCategories;
        [SerializeField] List<GameObject> bodyParts;

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
            
            //special case:
            //re-activate all t-shirt sleeves
            EnableDisableTshirtSleeves(true);
        }

        void EnableDisableTshirtSleeves(bool b) {
            foreach (var bodyPart in bodyParts.Where(bodyPart => bodyPart.name == "ShirtLeftSleeve" || bodyPart.name == "ShirtRightSleeve")) {
                bodyPart.gameObject.SetActive(b);
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

            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Shirts));
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Pants));
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Skirts));
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Jackets));
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Shoes));
            EventBroker.Instance().SendMessage(new EventClothesChanged(lastKnownClothes.Accessories));
            
            //re-activate all t-shirt sleeves
            EnableDisableTshirtSleeves(true);
        }


        void UpdateClothes(EventClothesChanged eventClothesChanged) {
            lastKnownClothes.hasReloadedScene--;
            if (eventClothesChanged.CombinedWearables == null) return;
            bool clothesRemoved = CompareAndRemoveSelectedClothes(eventClothesChanged.CombinedWearables);

            if (clothesRemoved) {
                //update last known clothes (set to null)
                var field = typeof(LastKnownClothes).GetField(eventClothesChanged.CombinedWearables.clothingType.name);
                if (field == null) {
                    throw new Exception("can't find the clothing type field in last known clothes!");
                }

                field.SetValue(lastKnownClothes, null);
                return;
            }

            EnableOrDisableCategory(eventClothesChanged.CombinedWearables);

            DressBodyParts(eventClothesChanged.CombinedWearables);

            SetLastKnownItemOfType(eventClothesChanged.CombinedWearables);
        }

        void DressBodyParts(CombinedWearables combinedWearable) {
            if (combinedWearable == null) return;
            //put on the different clothing parts on the different body parts
            foreach (var wearable in combinedWearable.wearable) {
                foreach (var bodyPart in bodyParts.Where(bodyPart => wearable.ClothingType.name == bodyPart.name)) {
                    foreach (var material in bodyPart.GetComponent<MeshRenderer>().materials) {
                        material.mainTexture = wearable.Texture;
                    }
                }
            }

            //Special case: When putting on a jacket -> Deactivate shirt sleeves
            if (combinedWearable.clothingType.name == "Jackets") {
                //deactivate the correct shirt sleeves (actually, de-activate all shirt sleeves, no-one will tell a difference)
                EnableDisableTshirtSleeves(false);
            }
        }

        void EnableOrDisableCategory(CombinedWearables combinedWearable) {
            //enable/disable the clothes categories, based on the incoming new clothes
            //for example: we get basic pants
            //for example:first disable all pants
            foreach (var category in clothesCategories) {
                if (category.name == combinedWearable.clothingType.name) {
                    category.SetActive(false);
                }
            }

            //for example:enable basic pants
            var rarity = clothingRarities.Find(i => i.name == combinedWearable.rarity.name);
            var categoryToShow = rarity.transform.Find(combinedWearable.clothingType.name);
            categoryToShow.gameObject.SetActive(true);

            //special case: not able to wear both skirts and pants at the same time
            //if incoming clothes is pants
            if (combinedWearable.clothingType.name == "Pants") {
                //then disable all skirts
                foreach (var category in clothesCategories) {
                    if (category.name == "Skirts")
                        category.SetActive(false);
                }
                //and remove skirts from last known
                lastKnownClothes.Skirts = null;
            }

            //if incoming clothes is skirts
            if (combinedWearable.clothingType.name == "Skirts") {
                //then disable all pants
                foreach (var category in clothesCategories) {
                    if (category.name == "Pants")
                        category.SetActive(false);
                }
                //and remove pants from last known
                lastKnownClothes.Pants = null;
            }
        }

        bool CompareAndRemoveSelectedClothes(CombinedWearables combinedWearable) {
            if (GetLastKnownItemOfType(combinedWearable.clothingType.name) == combinedWearable && !BiggerThanZero()) {
                RemoveClothesPiece(combinedWearable.rarity.name, combinedWearable.clothingType.name);

                //Special case: When removing a jacket -> activate shirt sleeves
                if (combinedWearable.clothingType.name == "Jackets") {
                    EnableDisableTshirtSleeves(true);
                }

                return true;
            }

            return false;
        }

        bool BiggerThanZero() {
            return lastKnownClothes.hasReloadedScene > 0;
        }

        void RemoveClothesPiece(string rarityName, string clothingTypeName) {
            var rarity = clothingRarities.Find(i => i.name == rarityName);
            var clothTransform = rarity.transform.Find(clothingTypeName);
            clothTransform.gameObject.SetActive(false);
        }

        CombinedWearables GetLastKnownItemOfType(string clothingTypeName) {
            var field = typeof(LastKnownClothes).GetField(clothingTypeName);
            if (field == null) {
                throw new Exception("can't find the clothing type in last known clothes!");
            }

            return field.GetValue(lastKnownClothes) as CombinedWearables;
        }

        void SetLastKnownItemOfType(CombinedWearables combinedWearable) {
            var field = typeof(LastKnownClothes).GetField(combinedWearable.clothingType.name);
            if (field == null) {
                throw new Exception("can't find the clothing type in last known clothes!");
            }

            field.SetValue(lastKnownClothes, combinedWearable);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventClothesChanged>(UpdateClothes);
            EventBroker.Instance().UnsubscribeMessage<RemoveAllClothes>(RemoveClothes);
        }
    }
}