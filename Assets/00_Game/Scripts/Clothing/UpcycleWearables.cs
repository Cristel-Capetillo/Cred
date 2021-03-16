using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing
{
    public class UpcycleWearables : MonoBehaviour
    {
        Wearable[] wearables = new Wearable[2];
        public GameObject[] clothingItems;
        InventoryButtonScript inventoryButtonScript;
        public int count = 0;
        public bool bothHasBeenCollected;
        public void FixedUpdate()
        {
            clothingItems = GameObject.FindGameObjectsWithTag("Clothing");
            if (clothingItems.Length > 0)
            {
                GetScript();
            }

            if (bothHasBeenCollected) {

                Debug.Log("Wearables: " + wearables[0] + ", " + wearables[1]);
            }
        }

        public void GetScript()
        {
            for (int i = 0; i < clothingItems.Length; i++)
            {
                inventoryButtonScript = clothingItems[i].GetComponent<InventoryButtonScript>();

                if (inventoryButtonScript.clothingChosen)
                {
                    
                    count++;
                    if (count == 1)
                    {
                        wearables[0] = inventoryButtonScript._wearable;
                    }
                    if(count == 2)
                    {
                        wearables[1] = inventoryButtonScript._wearable;
                        bothHasBeenCollected = true;
                        count = 0;
                    }

                    inventoryButtonScript.clothingChosen = false;


                }

            }
        }
    }
}