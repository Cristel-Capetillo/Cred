using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObjects/ClothingType")]
    public class ClothingType : ScriptableObject{
        [SerializeField] string singularName;
        
        public string SingularName => singularName;

      
    }
}