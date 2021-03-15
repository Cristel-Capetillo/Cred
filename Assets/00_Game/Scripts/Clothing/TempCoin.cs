using UnityEngine;

namespace Clothing {
    public class TempCoin : MonoBehaviour {
        public int coin;

        public void OnClickAddMoney() {
            coin += 10;
        }
    }
}