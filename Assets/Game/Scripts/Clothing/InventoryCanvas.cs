using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cred
{
    
    
    public class InventoryCanvas : MonoBehaviour {

        public void ToggleButton(GameObject gameObject) {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        

    }
}
