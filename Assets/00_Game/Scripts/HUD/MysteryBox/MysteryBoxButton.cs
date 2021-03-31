using System.Collections;
using Currency.Coins;
using MysteryBox;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace HUD.MysteryBox {
    public class MysteryBoxButton : MonoBehaviour {
        public GameObject selectorMenuPrefab;
        public Text amountText;
        public RectTransform parentCanvas;

        IEnumerator Start() {
            yield return new WaitForSeconds(3f);
            var amount = FindObjectOfType<MysteryBoxInventory>().Owned;
            amountText.text = amount > 0 ? $"{amount}" : " ";
            EventBroker.Instance().SubscribeMessage<EventMysteryBoxBought>(OnLootBoxPurchased);
            EventBroker.Instance().SubscribeMessage<EventMysteryBoxOpened>(OnLootBoxOpened);
        }

        void OnLootBoxOpened(EventMysteryBoxOpened eventMysteryBoxOpened) => UpdateLootBoxAmount();

        void OnLootBoxPurchased(EventMysteryBoxBought eventMysteryBoxBought) => UpdateLootBoxAmount();
        

        void UpdateLootBoxAmount() {
            var amount = FindObjectOfType<MysteryBoxInventory>().Owned;
            amountText.text = amount > 0 ? $"{amount}" : " ";
        }

        public void SpawnLootBoxSelectionMenu() {
            if (FindObjectOfType<SelectorMenu>()) {
                Debug.Log("MysteryBox Selector Menu already open.");
                return;
            }
            var instance = Instantiate(selectorMenuPrefab, parentCanvas);
        }
        
        void Update() {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.C)) {
                EventBroker.Instance().SendMessage(new EventUpdateCoins(300));
            }

            if (Input.GetKeyDown(KeyCode.L)) {
                EventBroker.Instance().SendMessage(new EventMysteryBoxBought());
            }
#endif
        }
    }
}
