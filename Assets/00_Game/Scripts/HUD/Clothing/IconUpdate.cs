using Clothing;
using UnityEngine;
using UnityEngine.UI;

namespace HUD.Clothing {
    public class IconUpdate : MonoBehaviour {
        CombinedWearables combinedWearables;
        Image[] images;
        public Sprite[] sprites;

        void Start() {
            combinedWearables = GetComponent<CombinedWearables>();
            images = GetComponentsInChildren<Image>();
            DisableImages();
            UpdateImages();
        }

        void DisableImages() {
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
        }

        void UpdateImages() {
            switch (combinedWearables.wearable.Count) {
                case 1:
                    images[1].sprite = combinedWearables.wearable[0].Sprite;
                    ActivateImage(1);
                    ValidateRarity();
                    break;
                case 2:
                    images[2].sprite = combinedWearables.wearable[1].Sprite;
                    images[3].sprite = combinedWearables.wearable[0].Sprite;
                    ActivateImage(2);
                    ValidateRarity();
                    break;
                case 3:
                    images[1].sprite = combinedWearables.wearable[0].Sprite;
                    images[2].sprite = combinedWearables.wearable[2].Sprite;
                    images[3].sprite = combinedWearables.wearable[1].Sprite;
                    ActivateImage(3);
                    ValidateRarity();
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
                images[0].sprite = sprites[0];
            }

            if (combinedWearables.rarity.name == "Normal") {
                images[0].sprite = sprites[1];
            }

            if (combinedWearables.rarity.name == "Designer") {
                images[0].sprite = sprites[2];
            }
        }
    }
}