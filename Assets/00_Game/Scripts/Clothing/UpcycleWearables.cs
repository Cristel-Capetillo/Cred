using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing
{
    public class UpcycleWearables : MonoBehaviour
    {
        Wearable[] wearables;
        public GameObject[] clothingItems;
        InventoryButtonScript inventoryButtonScript;
        public int count = 0;

        public void Update()
        {
            clothingItems = GameObject.FindGameObjectsWithTag("Clothing");
            if (clothingItems.Length > 0)
            {
                GetScript();
            }
        }

        public void GetScript()
        {
            for (int i = 0; i < clothingItems.Length; i++)
            {
                inventoryButtonScript = clothingItems[i].GetComponent<InventoryButtonScript>();

                if (inventoryButtonScript.hasBeenChosen)
                {
                    count++;
                    Debug.Log("Number of times Hasbeenchoosen is true: " + count);
                  
                    inventoryButtonScript.hasBeenChosen = false;


                }

            }
        }
    }
}