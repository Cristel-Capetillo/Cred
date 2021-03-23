using UnityEngine;

namespace Clothing {
    [CreateAssetMenu(menuName = "ScriptableObject/ClothingType")]
    public class ClothingType : ScriptableObject {
        [SerializeField] string singularName;
        
        public string SingularName => singularName;
    }
}