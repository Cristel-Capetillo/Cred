using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Clothing.Inventory {
    public class IconUpdate : MonoBehaviour {
        [SerializeField] Text stylePointsText;
        [SerializeField] Text amountText;
        [SerializeField] Image[] colorDataImages;
        CombinedWearables wearables;
        public Sprite[] sprites;
        public Image[] images;
        Image backgroundImage;

        int imagesToHaveActive;

        void Start() {
            EventBroker.Instance().SubscribeMessage<EventUpdateWearableHud>(UpdateImagesEvent);
        }

        void OnEnable() {
            wearables = GetComponent<CombinedWearables>();
            backgroundImage = GetComponent<Image>();
            UpdateImages();
        }

        void OnDestroy() {
            EventBroker.Instance().UnsubscribeMessage<EventUpdateWearableHud>(UpdateImagesEvent);
        }

        void UpdateImagesEvent(EventUpdateWearableHud afterInventoryLoad) {
            UpdateImages();
        }

        void DisableImages() {
            imagesToHaveActive = 0;
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
        }

        public void UpdateImages() {
            DisableImages();
            ValidateRarity();

            switch (wearables.wearable.Count) {
                case 1:
                    images[0].sprite = wearables.wearable[0].Sprite;
                    ActivateImage(1);
                    break;
                case 2:
                    images[1].sprite = wearables.wearable[1].Sprite;
                    images[2].sprite = wearables.wearable[0].Sprite;
                    ActivateImage(2);
                    break;
                case 3:
                    images[0].sprite = wearables.wearable[0].Sprite;
                    images[1].sprite = wearables.wearable[2].Sprite;
                    images[2].sprite = wearables.wearable[1].Sprite;
                    ActivateImage(3);
                    break;
            }

            UpdateInformation();
        }

        void ActivateImage(int amountToActivate) {
            switch (amountToActivate) {
                case 1:
                    images[0].enabled = true;
                    break;
                case 2:
                    images[1].enabled = true;
                    images[2].enabled = true;
                    break;
                case 3:
                    images[0].enabled = true;
                    images[1].enabled = true;
                    images[2].enabled = true;
                    break;
            }
        }

        void ValidateRarity() {
            switch (wearables.rarity.name) {
                case "Basic":
                    backgroundImage.sprite = sprites[0];
                    break;
                case "Normal":
                    backgroundImage.sprite = sprites[1];
                    break;
                case "Designer":
                    backgroundImage.sprite = sprites[2];
                    break;
            }
        }

        public void UpdateInformation() {
            if (wearables == null) return;
            stylePointsText.text = wearables.stylePoints.ToString();
            amountText.text = "x" + wearables.Amount;

            foreach (var image in colorDataImages) {
                image.enabled = false;
            }

            for (var i = 0; i < wearables.wearable.Count; i++) {
                var wearable = wearables.wearable[i];
                switch (i) {
                    case 0:
                        colorDataImages[i].enabled = true;
                        colorDataImages[i].color = wearable.colorData.color;
                        break;
                    case 1:
                        colorDataImages[i].enabled = true;
                        if (wearable.colorData == wearables.wearable[i - 1].colorData) {
                            colorDataImages[i].enabled = false;
                            continue;
                        }

                        colorDataImages[i].color = wearable.colorData.color;
                        break;

                    case 2:
                        colorDataImages[i].enabled = true;
                        if (wearable.colorData == wearables.wearable[i - 1].colorData) {
                            colorDataImages[i].enabled = false;
                            continue;
                        }

                        if (!colorDataImages[i - 1].enabled) {
                            colorDataImages[i - 1].enabled = true;
                            colorDataImages[i - 1].color = wearable.colorData.color;
                            break;
                        }

                        colorDataImages[i].color = wearable.colorData.color;
                        break;
                }
            }
        }
    }
}