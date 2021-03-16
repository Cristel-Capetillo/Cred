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
                    if (count == 0)
                    {
                        wearables[0] = inventoryButtonScript._wearable;
                    }
                    if(count == 1)
                    {
                        wearables[1] = inventoryButtonScript._wearable;
                    }
                    Debug.Log("Number of times Hasbeenchoosen is true: " + count);
                    Debug.Log("Wearable spot 1: "+  wearables[0]);
                    Debug.Log("Wearable spot 2: " + wearables[1]);


                    inventoryButtonScript.hasBeenChosen = false;


                }

            }
        }
    }
}