using System;
using UnityEngine;
using Utilities.Time;

namespace HUD.MysteryBox {
    public class MysteryBoxButton : MonoBehaviour {
        public GameObject selectorMenuPrefab;
        public RectTransform parentCanvas;

        TimeStamp freeMysteryBox;

        void Start() {
            freeMysteryBox = new TimeStamp("freeMysteryBox");
        }

        public void SpawnLootBoxSelectionMenu() {
            var instance = Instantiate(selectorMenuPrefab, parentCanvas);
            //instance.GetComponent<SelectorMenu>().GenerateMysteryBoxes(6);
        }
    }
}
