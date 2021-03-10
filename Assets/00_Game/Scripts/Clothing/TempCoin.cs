using UnityEngine;

namespace Cred._00_Game.Scripts.Clothing {
    public class TempCoin : MonoBehaviour {
        public int coin;

        public void OnClickAddMoney() {
            coin += 10;
        }
    }
}