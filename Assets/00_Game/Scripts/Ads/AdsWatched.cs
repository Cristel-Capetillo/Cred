using Cred._00_Game.Scripts;
using UnityEngine;

namespace Cred
{
    public class AdsWatched : MonoBehaviour {
        public void WatchedAdReward(int amount) {
            FindObjectOfType<Coin>().Coins += amount;
        }
    }
}
