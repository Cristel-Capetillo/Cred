using System;
using Currency.Coins;
using MysteryBox;
using UnityEngine;
using Utilities;

namespace HUD.MysteryBox {
    public class MysteryBoxButton : MonoBehaviour {
        public GameObject selectorMenuPrefab;
        public RectTransform parentCanvas;

        public void SpawnLootBoxSelectionMenu() {
            if (FindObjectOfType<SelectorMenu>()) {
                Debug.Log("MysteryBox Selector Menu already open.");
                return;
            }
            var instance = Instantiate(selectorMenuPrefab, parentCanvas);
        }
        
        void Update() {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F11)) {
                EventBroker.Instance().SendMessage(new EventUpdateCoins(300));
            }

            if (Input.GetKeyDown(KeyCode.F7)) {
                EventBroker.Instance().SendMessage(new EventMysteryBoxBought());
            }
#endif
        }
    }
}
