using Cred._00_Game.Scripts.Clothing;
using UnityEngine;

namespace Cred
{
    public class TypeOfClothing : MonoBehaviour
    {
        public Clothing clothing;
        public ClothingAmount clothingAmount;
        public TempCoin tempCoin;
        
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

          clothingAmount.clothings.Add(gameObject);
        }

        public void Update() {
            Debug.Log("isAffordable "+clothing.affordable);
            if (clothing.cost < tempCoin.coin) {
                Debug.Log("isAffordable2 "+clothing.affordable);
            }
        }
    }
}
