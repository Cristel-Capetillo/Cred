using System;
using Cred._00_Game.Scripts.Clothing;
using UnityEngine;
using UnityEngine.UI;

namespace Cred
{
    public class TypeOfClothing : MonoBehaviour
    {
        public Clothing clothing;
        public PlayerClothingAmount clothingAmount;
        public TempCoin tempCoin;
        public Text CoinsText;
        public GameObject buyButton;
        private void Start()
        {
            /*Debug.Log("Hipster preference: " + typeOfClothing.hipsterStyle);
            Debug.Log("Influencer preference: " + typeOfClothing.influencerStyle);
            Debug.Log("Raver preference: " + typeOfClothing.raverStyle);
            Debug.Log("the Hole Preference: " + typeOfClothing.raverClub);
            Debug.Log("the Niche Preference: " + typeOfClothing.hipsterClub);
            Debug.Log("the Yard Preference: " + typeOfClothing.yardClub);

            Debug.Log("Jeans: " + typeOfClothing.categoryJeans);
            Debug.Log("Hat: " + typeOfClothing.categoryHat);
            Debug.Log("Shirt: " + typeOfClothing.categoryShirt);

            Debug.Log("Deluxe clothing: " + typeOfClothing.deluxeClothing);
            Debug.Log("Standard clothing: " + typeOfClothing.standardClothing);*/

          clothingAmount.playerClothings.Add(gameObject);
          buyButton.SetActive(false);
        }

        public void Update() {
            if (tempCoin.coin >= clothing.cost) {
                clothing.affordable = true;
                buyButton.SetActive(true);
            }
            else {
                clothing.affordable = false;
                buyButton.SetActive(false);
            }
            CoinsText.text = tempCoin.coin.ToString();
            
        }
    }
}
