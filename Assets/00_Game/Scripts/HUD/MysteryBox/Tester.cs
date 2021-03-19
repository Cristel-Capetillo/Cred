using UnityEngine;

namespace HUD.MysteryBox {
    public class Tester : MonoBehaviour {
        public GameObject selectorMenuPrefab;
        public RectTransform parentCanvas;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F9)) {
                var instance = Instantiate(selectorMenuPrefab, parentCanvas);
                instance.GetComponent<SelectorMenu>().GenerateMysteryBoxes(6);
            }
        }
    }
}
