using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Inventory {
    public class IconUpdate : MonoBehaviour {
        CombinedWearables combinedWearables;
        public Image[] images;
        public Sprite[] sprites;

        Image backgroundImage;

        void Start() {
            combinedWearables = GetComponent<CombinedWearables>();
            images = GetComponentsInChildren<Image>();
            backgroundImage = GetComponent<Image>();
            EventBroker.Instance().SubscribeMessage<EventFinishedLoadingPlayerInventory>(UpdateImages);
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventFinishedLoadingPlayerInventory>(UpdateImages);
        }

        void DisableImages() {
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
        }

        void UpdateImages(EventFinishedLoadingPlayerInventory afterInventoryLoad) {
            DisableImages();
            ValidateRarity();

            switch (combinedWearables.wearable.Count) {
                case 1:
                    images[1].sprite = combinedWearables.wearable[0].Sprite;
                    ActivateImage(1);
                    break;
                case 2:
                    images[2].sprite = combinedWearables.wearable[1].Sprite;
                    images[3].sprite = combinedWearables.wearable[0].Sprite;
                    ActivateImage(2);
                    break;
                case 3:
                    images[1].sprite = combinedWearables.wearable[0].Sprite;
                    images[2].sprite = combinedWearables.wearable[2].Sprite;
                    images[3].sprite = combinedWearables.wearable[1].Sprite;
                    ActivateImage(3);
                    break;
            }
        }

        void ActivateImage(int amountToActivate) {
            switch (amountToActivate) {
                case 1:
                    images[1].enabled = true;
                    break;
                case 2:
                    images[2].enabled = true;
                    images[3].enabled = true;
                    break;
                case 3:
                    images[1].enabled = true;
                    images[2].enabled = true;
                    images[3].enabled = true;
                    break;
            }
        }

        void ValidateRarity() {
            if (combinedWearables.rarity.name == "Basic") {
                backgroundImage.sprite = sprites[0];
            }

            if (combinedWearables.rarity.name == "Normal") {
                backgroundImage.sprite = sprites[1];
            }

            if (combinedWearables.rarity.name == "Designer") {
                backgroundImage.sprite = sprites[2];
            }
        }
    }
}