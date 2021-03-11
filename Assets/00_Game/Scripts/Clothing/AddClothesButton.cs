using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cred
{
    public class AddClothesButton : MonoBehaviour
    {
        public GameObject clothing;
        public PlayerClothingAmount clothingAmount;
        public void onClickAddClothing()
        {
            GameObject go = Instantiate(clothing, clothing.transform.position, clothing.transform.rotation);
            clothingAmount.playerClothings.Add(go);
        }
    }
}