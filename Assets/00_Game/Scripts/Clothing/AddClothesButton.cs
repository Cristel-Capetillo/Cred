using UnityEngine;

namespace Clothing
{
    public class AddClothesButton : MonoBehaviour
    {
        public GameObject clothing;
        public void onClickAddClothing()
        {
            GameObject go = Instantiate(clothing, clothing.transform.position, clothing.transform.rotation);
        }
    }
}
