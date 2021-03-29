using System;
using Currency.Coins;
using MysteryBox;
using UnityEngine;
using Utilities;

namespace HUD.MysteryBox {
    public class PurchaseMysteryBox : MonoBehaviour {

        [SerializeField] int mysteryBoxPrice;
        Coin coins;

        void Start() {
            coins = FindObjectOfType<Coin>();
        }

        public void Purchase() {
            if (coins.Coins < mysteryBoxPrice) return;
            coins.Coins -= mysteryBoxPrice;
            EventBroker.Instance().SendMessage(new EventMysteryBoxBought());
        }
    }
}