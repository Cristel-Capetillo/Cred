using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cred
{
    public class TypeOfClothing : MonoBehaviour
    {
        public Clothing typeOfClothing;


        private void Start()
        {
            Debug.Log("Hipster preference: " + typeOfClothing.hipsterStyle);
            Debug.Log("Influencer preference: " + typeOfClothing.influencerStyle);
            Debug.Log("Raver preference: " + typeOfClothing.raverStyle);
            Debug.Log("the Hole Preference: " + typeOfClothing.raverClub);
            Debug.Log("the Niche Preference: " + typeOfClothing.hipsterClub);
            Debug.Log("the Yard Preference: " + typeOfClothing.yardClub);

            Debug.Log("Jeans: " + typeOfClothing.categoryJeans);
            Debug.Log("Hat: " + typeOfClothing.categoryHat);
            Debug.Log("Shirt: " + typeOfClothing.categoryShirt);


            Debug.Log("Deluxe clothing: " + typeOfClothing.deluxeClothing);
            Debug.Log("Standard clothing: " + typeOfClothing.standardClothing);

        }
    }
}
