using UnityEngine;

namespace HUD.MysteryBox {
    public class MysteryBoxButton : MonoBehaviour {
        public GameObject selectorMenuPrefab;
        public RectTransform parentCanvas;

        public void SpawnLootBoxSelectionMenu() {
            var instance = Instantiate(selectorMenuPrefab, parentCanvas);
            //instance.GetComponent<SelectorMenu>().GenerateMysteryBoxes(6);
        }
    }
}
