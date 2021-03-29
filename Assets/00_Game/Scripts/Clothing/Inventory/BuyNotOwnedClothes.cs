using System;
using UnityEngine;
using static Clothing.Inventory.PlayerInventory;

namespace Clothing.Inventory {
    public class BuyNotOwnedClothes : MonoBehaviour {
        bool notOwned;

        
        
        public void BuyClothes() {
            if (InventoryData.Amount.Length == 0) {
                OpenBuyClothesMenu();
            }
        }

        void OpenBuyClothesMenu() {
            throw new NotImplementedException();
        }
    }
}
